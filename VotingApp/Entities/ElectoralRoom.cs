using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("ElectoralRooms")]
    public class ElectoralRoom
    {
        [Key]
        [Column(Order = 4)]
        [Required]
        public int ElectoralRoomId { get; set; }

        [Required]
        [StringLength(25)]
        public string ElectoralRoomName { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
