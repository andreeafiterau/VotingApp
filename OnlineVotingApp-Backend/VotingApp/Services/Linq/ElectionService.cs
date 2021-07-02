using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;
using VotingApp.Services.Queries;

namespace VotingApp.Services
{
    public class ElectionService : IElectionInterface
    {
        private readonly MasterContext _context;
        public ElectionService(MasterContext context)
        {
            _context = context;
        }

        public void AddUsersForElection(ObjectForUsersFilter objectForUsersFilter,int IdElectoralRoom)
        {
            var filteredUsers=ElectionFilter.GetFilteredUsersForElection(objectForUsersFilter);

            foreach( User user in filteredUsers)
            {
                AddToKeylessTable.AddToTable_Election_Users(IdElectoralRoom, user.IdUser);
            }
        }

        //public IEnumerable<ElectionTypes> getElectionTypes()
        //{

        //}

        public IEnumerable<User> GetUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom)
        {
            List<User> filteredUsers = ElectionFilter.GetFilteredUsersForElection(objectForUsersFilter).ToList();

            var election_user = _context.Election_Users;
            var users = _context.Users;

            var result = from eu in election_user
                         join u in users on eu.IdUser equals u.IdUser
                         where eu.IdElectionType == IdElectoralRoom
                         select new User
                         {
                             IdUser = u.IdUser,
                             Username = u.Username,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             NrMatricol = u.NrMatricol,
                             Email = u.Email,
                             IsAccountActive = u.IsAccountActive
                         };

            List<User> finalRes = new List<User>();

            foreach(var u in result)
            {
                if (filteredUsers.Exists(user=>user.IdUser==u.IdUser))
                {
                    finalRes.Add(u);
                }
            }
            

            return finalRes;

        }

        public IEnumerable<Results> GetResults(int IdElectoralRoom)
        {
            var us = _context.Users;
            var cand = _context.Candidates;

            var candidates = from cc in cand
                             join  u in us on cc.IdUser equals u.IdUser
                    where cc.IdElectoralRoom == IdElectoralRoom
                    select new
                    {
                        cc.IdElectoralRoom,
                        cc.IdCandidate,
                        cc.IdUser,
                        u.FirstName,
                        u.LastName
                    };

            IList<Results> resultList = new List<Results>();

            foreach( var c in candidates)
            {
                int count=VoteCount.Count_Votes(c.IdCandidate);

                Results results = new Results(c.IdCandidate, c.FirstName + " " + c.LastName, count);

                resultList.Add(results);
            }

            return resultList;
        }

        private IEnumerable<ElectionViewForUser> GetElectoralRoomsForUser(int idUser)
        {
            return  from e in _context.ElectionTypes
                    join er in _context.ElectoralRooms on e.IdElectionType equals er.IdElectionType
                    select new ElectionViewForUser
                    {
                        IdElectoralRoom=er.IdElectoralRoom,
                        ElectionName=e.ElectionName,
                        ElectionDate=er.Date,
                        College=er.IdCollege,
                        Department=er.IdDepartment
                    };
        }

        public IEnumerable<ElectionViewForUser> GetPresentElectoralRooms(int idUser)
        {
            return GetElectoralRoomsForUser(idUser)
                   .Where(electoralRoom => 
                   DateTime.Now >= electoralRoom.ElectionDate &&
                   DateTime.Now <= electoralRoom.ElectionDate.Add(new TimeSpan(24, 0, 0)));
        }

        public IEnumerable<ElectionViewForUser> GetFutureElectoralRooms(int idUser)
        {
            return GetElectoralRoomsForUser(idUser)
                   .Where(electoralRoom =>
                   electoralRoom.ElectionDate > DateTime.Now);
        }

        public IEnumerable<ElectionViewForUser> GetPastElectoralRooms(int idUser)
        {
            return GetElectoralRoomsForUser(idUser)
                   .Where(electoralRoom =>
                   electoralRoom.ElectionDate < DateTime.Now.Date);
        }

        public IEnumerable<ElectionView> GetAllElectoralRooms()
        {
            //IList<ElectionView> electionViews = new List<ElectionView>();

            //var result= from er in _context.ElectoralRooms
            //       join et in _context.ElectionTypes on er.IdElectionType equals et.IdElectionType
            //       join c in _context.Candidates on er.IdElectoralRoom equals c.IdElectoralRoom
            //       join u in _context.Users on c.IdUser equals u.IdUser
            //       select new 
            //       {
            //           IdElectoralRoom = er.IdElectoralRoom,
            //           IdCandidate = c.IdCandidate,
            //           IdUser=u.IdUser,
            //           CandidateName = u.FirstName + " " + u.LastName,
            //           ElectionName = et.ElectionName,
            //           ElectionDate = er.Date,

            //       };

            //foreach(var r in result)
            //{
            //    if(electionViews.SingleOrDefault(view=>view.IdElectoralRoom==r.IdElectoralRoom)==null)
            //    {
            //        ElectionView electionView = new ElectionView();

            //        electionView.IdElectoralRoom = r.IdElectoralRoom;
            //        electionView.ElectionName = r.ElectionName;
            //        electionView.ElectionDate = r.ElectionDate;

            //        electionView.Candidates.Add(new CandidateView(r.IdCandidate, r.CandidateName,r.IdUser));

            //        electionViews.Add(electionView);
            //    }
            //    else
            //    {
            //        var election = electionViews.SingleOrDefault(view => view.IdElectoralRoom == r.IdElectoralRoom);

            //        election.Candidates.Add(new CandidateView(r.IdCandidate, r.CandidateName,r.IdUser));
            //    }
            //}

            //return electionViews;

           return from er in _context.ElectoralRooms
                   join et in _context.ElectionTypes on er.IdElectionType equals et.IdElectionType
                   join c in _context.Colleges on er.IdCollege equals c.IdCollege into coll
                   from co in coll.DefaultIfEmpty()
                   join d in _context.Departments on er.IdDepartment equals d.IdDepartment into dep
                   from de in dep.DefaultIfEmpty()
                   select new ElectionView
                   {
                       IdElectoralRoom = er.IdElectoralRoom,
                       ElectionName = et.ElectionName,
                       ElectionDate = er.Date,
                       College=co.CollegeName,
                       Department=de.DepartmentName
                   };
        }
    }
}