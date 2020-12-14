﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Activation_Codes")]
    public class Activation_Code
    {
        [Key]
        [Column(Order = 6)]
        public int IdCode { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }
    }
}