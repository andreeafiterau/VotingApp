using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Helpers
{
    public class UsersFilter
    {
        public static string CreateQueryString(ObjectForUsersFilter objectForUsersFilter)
        {
            string firstPart = "SELECT Users.IdUser, Username,FirstName,LastName,NrMatricol,Email,IsAccountActive," +
                                "Roles.IdRole,Roles.RoleName,Colleges.IdCollege,Colleges.CollegeName," +
                                "Departments.IdDepartment,Departments.DepartmentName FROM Users "+               
                                  "INNER JOIN Users_Roles on Users.IdUser = Users_Roles.IdUser " +
                                  "INNER JOIN Roles on Users_Roles.IdRole = Roles.IdRole " +
                                  "INNER JOIN Users_Departments on Users.IdUser = Users_Departments.IdUser " +
                                  "INNER JOIN Departments on Departments.IdDepartment = Users_Departments.IdDepartment "+
                                  "INNER JOIN Colleges on Departments.IdCollege = Colleges.IdCollege WHERE ";

            string secondPart = "";


            if (objectForUsersFilter.IdRole != -1)
            {
                secondPart += " AND Roles.IdRole=" + objectForUsersFilter.IdRole.ToString();
            }

            if (objectForUsersFilter.IdCollege != -1)
            {
                secondPart += " AND  Colleges.IdCollege=" + objectForUsersFilter.IdCollege.ToString();
            }

            if (objectForUsersFilter.IdDepartment != -1)
            {
                secondPart += " AND Departments.IdDepartment=" + objectForUsersFilter.IdDepartment.ToString();
            }

            return firstPart + secondPart.Substring(5);
        }

        public static IList<UserAdminView> GetFilteredUsers(ObjectForUsersFilter objectForUsersFilter)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-RPNBQ1M;Integrated Security=true;Database=VotingApp;"))
            {
                con.Open();

                List<UserAdminView> userAdminViews = new List<UserAdminView>();



                using (SqlCommand command = new SqlCommand(CreateQueryString(objectForUsersFilter), con))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       

                        int IdUser = reader.GetInt32(0);
                        string Username = reader.GetString(1);
                        string FirstName = reader.GetString(2);
                        string LastName = reader.GetString(3);
                        string NrMatricol = reader.GetString(4);
                        string Email = reader.GetString(5);
                        bool IsAccountActive = reader.GetBoolean(6);
                        int IdRole = reader.GetInt32(7);
                        string RoleName = reader.GetString(8);
                        int IdCollege = reader.GetInt32(9);
                        string CollegeName = reader.GetString(10);
                        int IdDepartment = reader.GetInt32(11);
                        string DepartmentName = reader.GetString(12);


                        if (!userAdminViews.Exists(u => u.User.IdUser == IdUser))
                        {
                            UserAdminView userAdminView = new UserAdminView();
                            userAdminView.User = new User(IdUser, FirstName, LastName, Username, NrMatricol, Email, IsAccountActive);
                            userAdminView.Role = new Role(IdRole, RoleName);
                            userAdminView.Departments.Add(new Department(IdDepartment, DepartmentName, IdCollege));
                            userAdminView.Colleges.Add(new College(IdCollege, CollegeName));

                            userAdminViews.Add(userAdminView);
                        }
                        else
                        {
                            var u = userAdminViews.Find(u => u.User.IdUser == IdUser);
                            if (!u.Colleges.Exists(d => d.IdCollege == IdCollege))
                            {
                                UserAdminView userAdminView = new UserAdminView();
                                userAdminView.Colleges.Add(new College(IdCollege, CollegeName));

                            }

                            if (!u.Departments.Exists(d => d.IdDepartment == IdDepartment))
                            {
                                UserAdminView userAdminView = new UserAdminView();
                                userAdminView.Departments.Add(new Department(IdDepartment, DepartmentName, IdCollege));

                            }

                        }
                    }
                }

                con.Close();

                return userAdminViews;

            }
        }
    }
}
