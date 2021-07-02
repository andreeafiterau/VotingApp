using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Services.Linq
{
    public class CollegeService: ICollegeInterface
    {

        private readonly MasterContext _context;
        public CollegeService(MasterContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetDepartmentsForCollegeId(int id)
        {
            return _context.Departments.Where(d => d.IdCollege == id);
        }

        public void DeleteCollege(int id)
        {

            AddToKeylessTable.DeleteFromTable_Departments(id);
     
            _context.Colleges.Remove(_context.Colleges.SingleOrDefault(c => c.IdCollege == id));

            _context.SaveChanges();
        }
        public IEnumerable<CollegeView> GetColleges()
        {
            
            List<CollegeView> collegeList = new List<CollegeView>();

            var result = from college in _context.Colleges
                         join department in _context.Departments on college.IdCollege equals department.IdCollege
                         select new CollegeView
                         {
                             IdCollege = college.IdCollege,
                             CollegeName = college.CollegeName,
                             Departments = new List<Department>() { new Department(department.IdDepartment, department.DepartmentName, college.IdCollege) }
                         };

            foreach (var r in result)
            {
                if (!collegeList.Exists(c=>c.IdCollege==r.IdCollege))
                {
                    CollegeView collegeView = new CollegeView();

                    collegeView.IdCollege = r.IdCollege;
                    collegeView.CollegeName = r.CollegeName;
                    collegeView.Departments.Add(new Department(r.Departments[0].IdDepartment, r.Departments[0].DepartmentName, r.Departments[0].IdCollege));
                    
                    collegeList.Add(collegeView);
                }
                else
                {
                    var u = collegeList.Find(u => u.IdCollege == r.IdCollege);
                   

                    if (!u.Departments.Exists(d => d.IdDepartment == r.Departments[0].IdDepartment))
                    {
                        
                        u.Departments.Add(new Department(r.Departments[0].IdDepartment, r.Departments[0].DepartmentName, r.Departments[0].IdCollege));

                    }

                }

            }

            return collegeList;
        }
    }
}
