using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VotingApp.Entities
{
    [Table("Electoral_Rooms")]
    public class Electoral_Room
    {
        [Key]
        [Column(Order = 6)]
        public int IdElectoralRoom { get; set; }


        [ForeignKey("Election_Types")]
        [Column(Order = 2)]
        public int IdElectionType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int IdCollege { get; set; }

        [Required]
        public int IdDepartment { get; set; }
    }
}
