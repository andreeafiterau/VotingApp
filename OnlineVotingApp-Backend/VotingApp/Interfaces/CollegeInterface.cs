using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface ICollegeInterface
    {
        IEnumerable<CollegeView> GetColleges();

        IEnumerable<Department> GetDepartmentsForCollegeId(int id);

        void DeleteCollege(int id);
    }
}
