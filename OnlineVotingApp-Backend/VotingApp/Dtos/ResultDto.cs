using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class ResultDto
    {
        public int IdCandidate { get; set; }

        public string CandidateName { get; set; }

        public int VoteCount { get; set; }
    }
}
