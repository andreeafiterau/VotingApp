using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("Election_Types")]
    public class ElectionTypes
    {
        [Key]
        [Column(Order = 2)]
        [Required]
        public int IdElectionType { get; set; }

        [Required]
        [StringLength(60)]
        public string ElectionName { get; set; }
    }
}
