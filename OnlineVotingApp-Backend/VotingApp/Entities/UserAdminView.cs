using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class UserAdminView
    {
        //public int IdUser { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string Username { get; set; }

        //public string NrMatricol { get; set; }

        //public string Email { get; set; }

        //public bool IsAccountActive { get; set; }

        public User User { get; set; }

        public Role Role  { get; set; }

        //map key=dep
        public List<College> Colleges { get; set; } 

        public List<Department> Departments { get; set; }
    }
}
