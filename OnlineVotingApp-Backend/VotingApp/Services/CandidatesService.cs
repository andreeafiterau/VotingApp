using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;

namespace VotingApp.Services
{
    public class CandidatesService
    {
        private readonly MasterContext _context;
        public CandidatesService(MasterContext context)
        {
            _context = context;
        }
        public IEnumerable<Candidate> GetCandidates(int IdElectoralRoom)
        {
            var cand = _context.Candidates;
            var us = _context.Users;
            var role = _context.Users_Roles;
            var el = _context.Election_Users;

            var candidates = from c in cand
                       join u in us on c.IdUser equals u.IdUser
                       join r in role on u.IdUser equals r.IdUser
                       join e in el on c.IdUser equals e.IdUser
                       where e.IdElectoralRoom == IdElectoralRoom
                       select new Candidate
                       {
                           IdCandidate = c.IdCandidate,
                           IdUser = c.IdUser
                       };

            return candidates;
        }

        public void AddCandidateOnElectoralRoom(int IdElectoralRoom,int IdCandidate)
        {
            var cand = _context.Candidates;

            foreach(var c in cand)
            {
                if(c.IdCandidate==IdCandidate)
                {
                    c.IdElectoralRoom = IdElectoralRoom;
                }
            }
        }

        public IEnumerable<Candidate> GetCandidatesForElection(int IdElectoralRoom)
        {
            var cand = _context.Candidates;

            IList<Candidate> candidatesForElection = new List<Candidate>();

            foreach(var c in cand)
            {
                if(c.IdElectoralRoom==IdElectoralRoom)
                {
                    candidatesForElection.Add(c);
                }

            }

            return candidatesForElection;
        }


    }
}
