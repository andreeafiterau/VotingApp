using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Users_Roles")]
    [Keyless]
    public class User_Role
    {
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

        [ForeignKey("Roles")]
        [Column(Order = 1)]
        public int IdRole { get; set; }
    }
}
