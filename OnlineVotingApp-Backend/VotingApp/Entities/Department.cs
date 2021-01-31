using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Departments")]
    public class Department
    {
        public Department() { }

        public Department (int idDepartment, string departmentName, int idCollege)
        {
            IdDepartment = idDepartment;
            DepartmentName = departmentName;
            IdCollege = idCollege;
        }

        [Key]
        [Column (Order = 3)]
        public int IdDepartment { get; set; }

        
        [StringLength(50)]
        public string DepartmentName { get; set; }

        [ForeignKey("Colleges")]
        [Column(Order =5)]
        public int IdCollege { get; set; }
    }
}
