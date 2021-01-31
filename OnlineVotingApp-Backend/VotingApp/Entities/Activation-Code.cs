using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Activation_Codes")]
    public class Activation_Code
    {
        public Activation_Code() { }

        public Activation_Code(string code, int idUser)
        {
            Code = code;
            IdUser = idUser;
        }


        [Key]
        [Column(Order = 6)]
        public int IdCode { get; set; }

        [Required]
        public string Code { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }
    }
}
