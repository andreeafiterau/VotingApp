using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class ElectionView
    {

        public ElectionView()
        {
            Candidates = new List<CandidateView>();
        }
        public int IdElectoralRoom { get; set; }

        public string ElectionName { get; set; }

        public DateTime ElectionDate { get; set; }

        public string College { get; set; }

        public string Department { get; set; }

        public List<CandidateView> Candidates { get; set; }
     
    }
}
