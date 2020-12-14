using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [Column (Order = 3)]
        public int IdDepartment { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }
    }
}
