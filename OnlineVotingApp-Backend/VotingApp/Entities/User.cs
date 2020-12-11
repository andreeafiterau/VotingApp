using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int IdUser { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string NrMatricol { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public bool IsAccountActive { get; set; }



    }
}
