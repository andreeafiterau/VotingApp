using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Entities
{
    public class PasswordToken
    {
        public PasswordToken() { }

        public PasswordToken(string token, int idUser)
        {
            Token = token;
            IdUser = idUser;
        }


        [Key]
        [Column(Order = 7)]
        public int IdToken { get; set; }

        [Required]
        public string Token { get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }
    }
}
