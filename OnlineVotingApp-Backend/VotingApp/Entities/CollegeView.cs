using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class CollegeView
    {
        public int IdCollege { get; set; }

        public string CollegeName { get; set; }

        public List<Department> Departments { get; set; }


        public CollegeView()
        {
            Departments = new List<Department>();
        }
    }
}
