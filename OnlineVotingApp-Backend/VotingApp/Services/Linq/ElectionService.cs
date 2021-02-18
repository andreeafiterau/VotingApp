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

        public IEnumerable<User> GetUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom)
        {
            List<User> filteredUsers = ElectionFilter.GetFilteredUsersForElection(objectForUsersFilter).ToList();

            var election_user = _context.Election_Users;
            var users = _context.Users;

            var result = from eu in election_user
                         join u in users on eu.IdUser equals u.IdUser
                         where eu.IdElectoralRoom == IdElectoralRoom
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
    }
}
