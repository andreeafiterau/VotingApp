using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Users_Departments")]
    [Keyless]
    public class User_Department
    {
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

        [ForeignKey("Departments")]
        [Column(Order = 3)]
        public int IdDepartment { get; set; }
    }
}
