using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface IUsersInterface 
    {
        User ForgotPasswordUpdate(User user, string password, string passwordToken);
        void AddPasswordTokenToTable(string token,int IdUser);
        void SendPasswordToken(string Email, string PasswordToken);
        bool VerifyPasswordToken(string token, int IdUser);
        User FindUserForActivation(string username);
        int FindUser(string username);
        void SendActivationCode(string email, string activationCode);
        void AddActivationKeyToTable(string Key, int IdUser);
        string CreateActivationKey();
        void ChangePassword(string username,string password);
        User Authenticate(string username, string password);
        User ActivateUser(User user, string password, string activationCode);
        IEnumerable<UserAdminView> GetAllUsersForAdmin(ObjectForUsersFilter objectForUsersFilter);
        void AddUsers(UserAdminView userAdminView);
        void UpdateUsersForAdmin(UserAdminView userAdminView);
        void DeleteUserForAdmin(int id);
    }
}
