using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Users_Roles")]
    [Keyless]
    public class User_Role
    {
        public User_Role() { }

        public User_Role(int idUser, int idRole)
        {
            IdUser = idUser;
            IdRole = idRole;
        }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

        [ForeignKey("Roles")]
        [Column(Order = 1)]
        public int IdRole { get; set; }
    }
}
