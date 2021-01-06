using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class UserAdminViewDto
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string NrMatricol { get; set; }

        public string Email { get; set; }

        public bool IsAccountActive { get; set; }

        public int IdRole { get; set; }

        public string RoleName { get; set; }

        public int IdCollege { get; set; }

        public string CollegeName { get; set; }

        public int IdDepartment { get; set; }

        public string DepartmentName { get; set; }
    }
}
