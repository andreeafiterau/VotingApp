using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class ElectionViewForUser
    {      
        public int IdElectoralRoom { get; set; }

        public string ElectionName { get; set; }

        public DateTime ElectionDate { get; set; }

        public int College { get; set; }

        public int Department { get; set; }

    }
}
