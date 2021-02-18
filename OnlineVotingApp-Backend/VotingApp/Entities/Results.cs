using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class Results
    {

        public Results(int idC,string name, int count)
        {
            IdCandidate = idC;
            CandidateName = name;
            VoteCount = count;
        }

        public int IdCandidate { get; set; }

        public string CandidateName { get; set; }

        public int VoteCount { get; set; }
    }
}
