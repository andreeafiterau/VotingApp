using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [StringLength(25)]
        public string Password { get; set; }

        [Required]
        [ForeignKey("ApplicationRoles")]
        [Column(Order = 1)]
        public int IdApplicationRole { get; set; }

    }
}
