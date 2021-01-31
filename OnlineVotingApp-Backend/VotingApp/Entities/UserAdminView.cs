using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class UserAdminView
    {
        public UserAdminView()
        {
            User = new User();
            Role = new Role();
            Colleges = new List<College>();
            Departments = new List<Department>();
        }
        public User User { get; set; }

        public Role Role  { get; set; }

        //map key=dep
        public List<College> Colleges { get; set; } 

        public List<Department> Departments { get; set; }
    }
}
