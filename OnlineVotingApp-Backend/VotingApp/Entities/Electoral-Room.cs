using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VotingApp.Entities
{
    [Table("Electoral_Rooms")]
    public class Electoral_Room
    {
        [Key]
        [Column(Order = 2)]
        [Required]
        public int IdElectoralRoom { get; set; }

        [Required]
        [StringLength(60)]
        public string ElectoralRoomName { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
