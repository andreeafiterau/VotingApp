using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface ICandidateInterface
    {
        IEnumerable<CandidateView> GetCandidatesForAdmin(List<Candidate> candidateCurrentList);
        IEnumerable<CandidateView> GetCandidates(int IdElectoralRoom);

        //void AddCandidateOnElectoralRoom(int IdElectoralRoom, int IdCandidate);
    }
}
