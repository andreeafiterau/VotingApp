using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Colleges")]
    public class College
    {
        [Key]
        [Column(Order = 4)]
        public int IdCollege { get; set; }

        [Required]
        [StringLength(50)]
        public string CollegeName { get; set; }
    }
}
