using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("Candidates")]
    public class Candidate
    {
        [Key]
        [Column(Order = 3)]
        [Required]
        public int CandidateId { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        [Required]
        public int UserId { get; set; }

        [ForeignKey("ElectoralRooms")]
        [Column(Order = 4)]
        [Required]
        public int ElectoralRoomId { get; set; }
    }
}
