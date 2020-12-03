using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class ElectoralRoomDto
    {
        public int IdElectoralRoom { get; set; }

        public string ElectoralRoomName { get; set; }

        public bool isActive { get; set; }
    }
}
