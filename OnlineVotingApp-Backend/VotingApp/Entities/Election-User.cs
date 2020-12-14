using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Elections_Users")]
    [Keyless]
    public class Election_User
    {
        [ForeignKey("Electoral_Rooms")]
        [Column(Order = 2)]
        public int IdElectoralRoom { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }
    }
}
