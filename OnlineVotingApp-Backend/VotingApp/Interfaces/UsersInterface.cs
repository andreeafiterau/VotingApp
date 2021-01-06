using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface IUsersInterface 
    {
        //User Authenticate(string username, string password);

        //User Create(User user, string password);

        IEnumerable<UserAdminView> GetAllUsersForAdmin(ObjectForUsersFilter objectForUsersFilter);

        void AddUsers(UserAdminView userAdminView);

        void UpdateUsersForAdmin(UserAdminView userAdminView);

        void DeleteUserForAdmin(int id);
    }
}
