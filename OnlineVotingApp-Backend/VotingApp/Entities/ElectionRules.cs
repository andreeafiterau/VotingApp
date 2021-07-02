using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("ElectionRules")]
    [Keyless]
    public class ElectionRules
    {
        [ForeignKey("ElectionTypes")]
        [Column(Order = 2)]
        public int IdElectionType { get; set; }

        [ForeignKey("Roles")]
        [Column(Order = 1)]
        public int IdRole { get; set; }

        [ForeignKey("Colleges")]
        [Column(Order = 4)]
        public int IdCollege { get; set; }

        [ForeignKey("Departments")]
        [Column(Order = 3)]
        public int IdDepartment { get; set; }
    }
}
