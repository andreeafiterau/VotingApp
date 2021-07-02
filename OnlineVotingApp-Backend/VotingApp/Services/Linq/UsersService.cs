using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Services
{
    public class UsersService : IUsersInterface
    {
        private MasterContext _context;
        private IRepository<User> _userRepo;
        private IRepository<Activation_Code> _activationCodeRepo;
        private IRepository<PasswordToken> _passwordTokenRepo;
        

        public UsersService(MasterContext context,IRepository<User> userRepo,IRepository<Activation_Code> activationCodeRepo,IRepository<PasswordToken> passwordTokenRepo)
        {
            _context = context;
            _userRepo = userRepo;
            _activationCodeRepo = activationCodeRepo;
            _passwordTokenRepo = passwordTokenRepo;
           

        }//CONSTRUCTOR

        //public IEnumerable<Role> getRole(int idUser)
        //{
        //    //var users = _context.Users;
        //    var users_roles = _context.Users_Roles;
        //    var roles = _context.Roles;

        //    var result = from ur in users_roles
        //                 join r in roles on ur.IdRole equals r.IdRole
        //                 where ur.IdUser == idUser
        //                 select new Role
        //                 {
        //                     IdRole = ur.IdRole,
        //                     RoleName = r.RoleName

        //                 };

        //    return result;

        //}

        public IEnumerable<UserAdminView> GetAllUsers()
        {
            List<UserAdminView> userAdminViews = new List<UserAdminView>();

            var result = from users in _context.Users
                         join r_users in _context.Users_Roles on users.IdUser equals r_users.IdUser
                         join roles in _context.Roles on r_users.IdRole equals roles.IdRole
                         join d_users in _context.Users_Departments on users.IdUser equals d_users.IdUser
                         join departments in _context.Departments on d_users.IdDepartment equals departments.IdDepartment
                         join coleges in _context.Colleges on departments.IdCollege equals coleges.IdCollege
                         select new UserAdminView
                         {
                             
                             User = new User(users.IdUser,users.Username, users.FirstName, users.LastName, users.NrMatricol, users.Email,users.IsAccountActive),
                             Role =new Role(roles.IdRole,roles.RoleName),
                             Colleges=new List<College>() { new College(coleges.IdCollege, coleges.CollegeName) },
                             Departments=new List<Department>() { new Department(departments.IdDepartment, departments.DepartmentName, coleges.IdCollege) }                           
                         };

            foreach(var r in result)
            {
                if (!userAdminViews.Exists(u => u.User.IdUser == r.User.IdUser))
                {
                    UserAdminView userAdminView = new UserAdminView();
                    userAdminView.User = new User(r.User.IdUser, r.User.Username, r.User.FirstName, r.User.LastName, r.User.NrMatricol, r.User.Email, r.User.IsAccountActive);
                    userAdminView.Role = new Role(r.Role.IdRole, r.Role.RoleName);
                    userAdminView.Departments.Add(new Department(r.Departments[0].IdDepartment, r.Departments[0].DepartmentName, r.Departments[0].IdCollege));
                    userAdminView.Colleges.Add(new College(r.Colleges[0].IdCollege, r.Colleges[0].CollegeName));

                    userAdminViews.Add(userAdminView);
                }
                else
                {
                    var u = userAdminViews.Find(u => u.User.IdUser == r.User.IdUser);
                    if (!u.Colleges.Exists(d => d.IdCollege == r.Colleges[0].IdCollege))
                    {
                        UserAdminView userAdminView = new UserAdminView();
                        userAdminView.Colleges.Add(new College(r.Colleges[0].IdCollege, r.Colleges[0].CollegeName));

                    }

                    if (!u.Departments.Exists(d => d.IdDepartment == r.Departments[0].IdDepartment))
                    {
                        UserAdminView userAdminView = new UserAdminView();
                        userAdminView.Departments.Add(new Department(r.Departments[0].IdDepartment, r.Departments[0].DepartmentName, r.Departments[0].IdCollege));

                    }

                }
            }

            return userAdminViews;
        }

        public UserAdminView Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;//arg null

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            var users_roles = _context.Users_Roles.SingleOrDefault(user_roles => user_roles.IdUser == user.IdUser);
            var role = _context.Roles.SingleOrDefault(role => role.IdRole == users_roles.IdRole);

            if (role.IdRole != 1)
            {
                var user_departments = _context.Users_Departments.SingleOrDefault(user_dep => user_dep.IdUser == user.IdUser);
                var departments = _context.Departments.SingleOrDefault(dep => dep.IdDepartment == user_departments.IdDepartment);

                var colleges = _context.Colleges.SingleOrDefault(coll => coll.IdCollege == departments.IdCollege);

                List<Department> dep = new List<Department>() { departments };
                List<College> coll = new List<College>() { colleges };

                return new UserAdminView(user, role, dep, coll);
            }
            else if(role.IdRole==1)
            {

                List<Department> dep = new List<Department>() { new Department() };
                List<College> coll = new List<College>() {new College() };

                return new UserAdminView(user, role, dep, coll);
            }
            return null;
            

        }//METHOD Authenticate

        public User ForgotPasswordUpdate(User user,string password, string passwordToken)
        {
            if (!VerifyPasswordToken(passwordToken, user.IdUser))
                throw new Exception("Password Token is not correct");

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsAccountActive = true;

            _context.Users.Update(user);

            var tokenEntry = _context.PasswordTokens.SingleOrDefault(token => token.IdUser == user.IdUser);

            _context.Remove(tokenEntry);

            _context.SaveChanges();

            return user;
        }

        public User ActivateUser(User user, string password, string activationCode)
        {
            if(!VerifyActivationCode(activationCode,user.IdUser))
                throw new Exception("Activation Code is not correct");

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsAccountActive = true;

            _context.Users.Update(user);

            var codeEntry=_context.Activation_Codes.SingleOrDefault(code => code.IdUser == user.IdUser);

            _context.Remove(codeEntry);

            _context.SaveChanges();

            //activation code delete after user id

            return user;

        }//METHOD Create

        public User FindUserForActivation(string Email)
        {
            return _context.Users.SingleOrDefault(user => user.Email == Email);
        }

       
        public string CreateActivationKey()
        {
            var activationKey = Guid.NewGuid().ToString();

            //act code.getall poate sa fie null
            var activationKeyAlreadyExists = _activationCodeRepo.GetAll().Any(a => a.Code == activationKey);

            if (activationKeyAlreadyExists)
            {
                activationKey = CreateActivationKey();
            }

            return activationKey;
        }

        public void AddActivationKeyToTable(string Key,int IdUser)
        {

            if(_context.Activation_Codes.FirstOrDefault(user=>user.IdUser==IdUser)==null)
            {
                Activation_Code activation_code = new Activation_Code(Key, IdUser);
                _activationCodeRepo.Insert(activation_code);
            }
            else
            {
                throw new Exception("There is an entry with this user id");
            }

        }

        //public bool IsUserActive(int idUser)
        //{
        //    if(_userRepo.GetByID(idUser).IsAccountActive==true)
        //    {
        //        return true;               
        //    }
        //    return false;
        //}

        public void AddPasswordTokenToTable(string Token,int IdUser)
        {
            if (IsUserActive(IdUser))
            {
                PasswordToken passwordToken = new PasswordToken(Token, IdUser);
                _passwordTokenRepo.Insert(passwordToken);
            }
            else
            {
                throw new Exception("The user is not active");
            }
           
        }

        public bool VerifyActivationCode(string Key,int idUser)
        {
           
            var codeFromTable =_context.Activation_Codes.SingleOrDefault(user => user.IdUser == idUser).Code;
                
            if (codeFromTable != Key)
            {
                    return false;
            }

            return true;
           
                       
        }

        public bool IsUserActive(int idUser)
        {
            return _context.Users.SingleOrDefault(user => user.IdUser == idUser).IsAccountActive;
        }

        public bool VerifyPasswordToken(string Token, int IdUser)
        {
            //get after idUser
            var codeFromTable = _context.PasswordTokens.SingleOrDefault(user => user.IdUser == IdUser).Token;

            if (codeFromTable != Token)
            {
                return false;
            }

            return true;
        }

        public void ChangePassword(string username,string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
                throw new Exception("Password or username is required");

            User user = new User();
            user = _context.Users.SingleOrDefault(user => user.Username == username);

            if (user == null)
                throw new Exception("Username incorrect");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public int FindUser(string email)
        {
            var user= _context.Users.SingleOrDefault(user => user.Email == email);

            if (user == null)
                   throw new Exception("Email was not found in the database");//explicit : nu exista emailul in baza de date

            return user.IdUser;
        }

        public void SendActivationCode(string email,string activationCode)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("andreea.fiterau96@gmail.com", "Fa04volei/ro"),
                EnableSsl = true
            };

            smtpClient.Send("andreea.fiterau96@gmail.com",email, "ActivationCode", "The activation code is:"+ activationCode);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }//METHOD CreatePasswordHash

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


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

            AddToKeylessTable.AddToTable_User_Role(user.IdUser, userAdminView.Role.IdRole);

            
            foreach(var department in userAdminView.Departments)
            {
                //User_Department user_Department = new User_Department(user.IdUser,department.IdDepartment);
                AddToKeylessTable.AddToTable_User_Department(user.IdUser, department.IdDepartment);
            }

           

            _context.SaveChanges();

        }

        //poate sa faca upd daca userul nu si a activat contul,daca e activat pate sa mod facultatea,dep,rol
        public void UpdateUsersForAdmin(UserAdminView userAdminView)
        {

                _userRepo.Update(new User(userAdminView.User.IdUser,userAdminView.User.Username, userAdminView.User.FirstName,
                                              userAdminView.User.LastName, userAdminView.User.NrMatricol,
                                              userAdminView.User.Email, false));


                //poate mai avea si mai multe roluri
                AddToKeylessTable.UpdateTable_User_Role(userAdminView.User.IdUser, userAdminView.Role.IdRole);


                foreach (var department in userAdminView.Departments)
                {

                    AddToKeylessTable.UpdateTable_User_Department(userAdminView.User.IdUser, department.IdDepartment);

                }

            _context.SaveChanges();

        }

        //cascade delete,votes,user-election,cadidates
        public void DeleteUserForAdmin(int id)
        {
            var user_role = _context.Users_Roles;
            var user_dep = _context.Users_Departments;
            var user = _context.Users;

            AddToKeylessTable.DeleteFromTable_Users_Roles(id);
            AddToKeylessTable.DeleteFromTable_Users_Departments(id);
            AddToKeylessTable.DeleteFromTable_Candidates(id);
            AddToKeylessTable.DeleteFromTable_Elections_Users(id);
            _context.Users.Remove(user.SingleOrDefault(d => d.IdUser == id));

            _context.SaveChanges();

        }

        public void SendPasswordToken(string Email, string PasswordToken)
        {
           
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("andreea.fiterau96@gmail.com", "Fa04volei/ro"),
                EnableSsl = true
            };

            smtpClient.Send("andreea.fiterau96@gmail.com", Email, "Password Token", "The password token is: " + PasswordToken);
        }
    }
}

