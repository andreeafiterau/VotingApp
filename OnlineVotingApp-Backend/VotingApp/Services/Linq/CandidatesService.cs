using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Services
{
    public class CandidatesService : ICandidateInterface
    {
        private readonly MasterContext _context;
        public CandidatesService(MasterContext context)
        {
            _context = context;
        }

        //choose candidates from list (posible candidates for each electoral room), sa afiseze si numele camerei electorale
        // un nou view pt functia asta, separat pe functii,variabile explicite
        public IEnumerable<CandidateView> GetCandidates(int IdElectoralRoom)
        {
            var cand = _context.Candidates;
            var us = _context.Users;
            var role = _context.Users_Roles;
            var el = _context.ElectoralRooms;

            var candidates = from c in cand
                       join u in us on c.IdUser equals u.IdUser
                       join e in el on c.IdElectoralRoom equals e.IdElectoralRoom
                       where e.IdElectoralRoom == IdElectoralRoom
                       select new CandidateView
                       {
                           IdCandidate = c.IdCandidate,
                           IdUser = c.IdUser,
                           IdElectoralRoom=e.IdElectoralRoom,
                           FirstName=u.FirstName,
                           LastName=u.LastName
                       };

            return candidates;
        }

        public IEnumerable<CandidateView> GetCandidatesForAdmin(List<Candidate> candidateCurrentList)
        {
            List<CandidateView> possibleCandidates = new List<CandidateView>();

            foreach(var c in _context.Users)
            {
                if(candidateCurrentList.SingleOrDefault(candidate=>candidate.IdUser==c.IdUser)==null)
                {
                    CandidateView tmp = new CandidateView();

                    tmp.IdUser = c.IdUser;
                    tmp.FirstName = c.FirstName + " " + c.LastName;

                    possibleCandidates.Add(tmp);
                }
            }

            return possibleCandidates;
        }

        public void Vote(int IdCandidate,int IdUser)
        {
            AddToKeylessTable.AddToTable_Vote(IdCandidate, IdUser);
        }

        //// add electoral for each specific candidate
        //public void AddCandidateOnElectoralRoom(int IdElectoralRoom,int IdCandidate)
        //{
        //    var cand = _context.Candidates;

        //    foreach(var c in cand)
        //    {
        //        if(c.IdCandidate==IdCandidate)
        //        {
        //            c.IdElectoralRoom = IdElectoralRoom;
        //        }
        //    }
        //}

    }
}
