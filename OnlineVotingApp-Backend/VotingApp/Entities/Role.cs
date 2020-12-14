
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int IdRole { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
