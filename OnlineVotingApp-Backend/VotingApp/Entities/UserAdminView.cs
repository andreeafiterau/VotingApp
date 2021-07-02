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

        public UserAdminView(User user,Role role,List<Department> dep,List<College> coll)
        {
            User = user;
            Role = role;
            Colleges = coll;
            Departments = dep;
        }
        public User User { get; set; }

        public Role Role  { get; set; }

        //map key=dep
        public List<College> Colleges { get; set; } 

        public List<Department> Departments { get; set; }
    }
}
