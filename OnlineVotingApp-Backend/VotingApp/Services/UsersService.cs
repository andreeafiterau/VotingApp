using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Services
{
    public class UsersService : IUsersInterface
    {
        private MasterContext _context;
        private IRepository<User> _userRepo;

        public UsersService(MasterContext context,IRepository<User> userRepo)
        {
            _context = context;
            _userRepo = userRepo;

        }//CONSTRUCTOR

        //public User Authenticate(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return null;

        //    var user = _context.Users.SingleOrDefault(x => x.Username == username);

        //    // check if username exists
        //    if (user == null)
        //        return null;

        //    // check if password is correct
        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    // authentication successful
        //    return user;

        //}//METHOD Authenticate

        //public User Create(User user, string password)
        //{
        //    // validation
        //    if (string.IsNullOrWhiteSpace(password))
        //        throw new Exception("Password is required");

        //    if (_context.Users.Any(x => x.Username == user.Username))
        //        throw new Exception("Username \"" + user.Username + "\" is already taken");

        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    user.IsAccountActive = true;

        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return user;

        //}//METHOD Create

        //public void Update(User userParam, string password = null)
        //{
        //    var user = _context.Users.Find(userParam.IdUser);

        //    if (user == null)
        //        throw new Exception("User not found");

        //    if (userParam.Username != user.Username)
        //    {
        //        // username has changed so check if the new username is already taken
        //        if (_context.Users.Any(x => x.Username == userParam.Username))
        //            throw new Exception("Username " + userParam.Username + " is already taken");
        //    }

        //    // update user properties
        //    user.Email = userParam.Email;
        //    user.Username = userParam.Username;
        //    user.IsAccountActive = userParam.IsAccountActive;

        //    // update password if it was entered
        //    if (!string.IsNullOrWhiteSpace(password))
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //        user.PasswordHash = passwordHash;
        //        user.PasswordSalt = passwordSalt;
        //    }

        //    _context.Users.Update(user);
        //    _context.SaveChanges();

        //}//METHOD Update

        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }

        //}//METHOD CreatePasswordHash

        //private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        //    if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        //    if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != storedHash[i]) return false;
        //        }
        //    }

        //    return true;
        //}

        
        public IEnumerable<UserAdminView> GetAllUsersForAdmin(ObjectForUsersFilter objectForUsersFilter)
        {
            return UsersFilter.GetFilteredUsers(objectForUsersFilter);
        }

        //poate sa adauge si numele,prenumele etc...
        public void AddUsers(UserAdminView userAdminView)
        {
            User user = new User(userAdminView.User.Username,userAdminView.User.FirstName,
                                 userAdminView.User.LastName,userAdminView.User.NrMatricol, userAdminView.User.Email,false);
            
            _userRepo.Insert(user);


            User_Role user_role = new User_Role(userAdminView.Role.IdRole,user.IdUser);

            
            _context.Add(user_role);

            
            foreach(var department in userAdminView.Departments)
            {
                User_Department user_Department = new User_Department(user.IdUser,department.IdDepartment);
                _context.Add(user_Department);
            }

           

            _context.SaveChanges();

        }

        //poate sa faca upd daca userul nu si a activat contul,daca e activat pate sa mod facultatea,dep,rol
        public void UpdateUsersForAdmin(UserAdminView userAdminView)
        {
            if(userAdminView.User.IsAccountActive)
            {
               
                var temp1 = _context.Users_Roles.Where(u => u.IdUser == userAdminView.User.IdUser)
                          .Select(u => new User_Role(userAdminView.Role.IdRole, userAdminView.User.IdUser))
                          .ToList();

               
                foreach (var department in userAdminView.Departments)
                {
                   
                    var temp2 = _context.Users_Departments.Where(d => d.IdUser == userAdminView.User.IdUser)
                          .Select(d => new User_Department(userAdminView.User.IdUser, department.IdDepartment))
                          .ToList();
                    
                }

                _context.SaveChanges();
            }
            else
            {

                var temp = _context.Users.Where(u => u.IdUser == userAdminView.User.IdUser)
                         .Select(u => new User(userAdminView.User.Username, userAdminView.User.FirstName,
                                              userAdminView.User.LastName, userAdminView.User.NrMatricol,
                                              userAdminView.User.Email, false))
                         .ToList();


                var temp1 = _context.Users_Roles.Where(u => u.IdUser == userAdminView.User.IdUser)
                          .Select(u => new User_Role(userAdminView.Role.IdRole, userAdminView.User.IdUser))
                          .ToList();


                foreach (var department in userAdminView.Departments)
                {

                    var temp2 = _context.Users_Departments.Where(d => d.IdUser == userAdminView.User.IdUser)
                          .Select(d => new User_Department(userAdminView.User.IdUser, department.IdDepartment))
                          .ToList();

                }

                _context.SaveChanges();
            }

        }

        public void DeleteUserForAdmin(int id)
        {
            var user_role = _context.Users_Roles;
            var user_dep = _context.Users_Departments;
            var user = _context.Users;

            _context.Remove(user_role.Find(id));
            _context.Remove(user_dep.Find(id));
            _context.Remove(user.Find(id));

            _context.SaveChanges();

        }

    }
}

