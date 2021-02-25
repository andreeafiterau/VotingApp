using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class PasswordTokenDto
    {
        public int IdToken { get; set; }

        public string Token { get; set; }

        public int IdUser { get; set; }
    }
}
