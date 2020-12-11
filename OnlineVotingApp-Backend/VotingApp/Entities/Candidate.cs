using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Candidates")]
    public class Candidate
    {
        [Key]
        [Column(Order = 5)]
        [Required]
        public int IdCandidate { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        [Required]
        public int IdUser { get; set; }

        [ForeignKey("Electoral_Rooms")]
        [Column(Order = 2)]
        [Required]
        public int ElectoralRoomId { get; set; }
    }
}
