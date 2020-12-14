using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface IUsersInterface 
    {
        User Authenticate(string username, string password);

        User Create(User user, string password);
    }
}
