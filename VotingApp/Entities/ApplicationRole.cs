using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    [Table("ApplicationRoles")]
    public class ApplicationRole
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int ApplicationRoleId { get; set; }

        [Required]
        [StringLength(25)]
        public string ApplicationName { get; set; }
    }
}
