using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace VotingApp.Entities
{
    [Table("Votes")]
    [Keyless]
    public class Vote
    {
        [ForeignKey("Users")]
        [Column(Order = 0 )]
        public int IdUser { get; set; }

        [ForeignKey("Candidates")]
        [Column(Order = 5)]
        public int IdCandidate { get; set; }
    }
}
