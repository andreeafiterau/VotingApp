using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Dtos
{
    public class UserAdminViewDto
    {
        public User User { get; set; }

        public Role Role { get; set; }

        //map key=dep
        public List<College> Colleges { get; set; }

        public List<Department> Departments { get; set; }
    }
}
