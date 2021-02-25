using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class UserActivationViewDto
    {
       
        public string Email { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }
}
