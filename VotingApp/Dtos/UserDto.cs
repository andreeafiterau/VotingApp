using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class UserDto
    {

        public int IdUser { get; set; }

        public string Username { get; set; }

        public byte PasswordSalt { get; set; }

        public byte PasswordHash { get; set; }

        public int IdApplicationRole { get; set; }
    }
}
