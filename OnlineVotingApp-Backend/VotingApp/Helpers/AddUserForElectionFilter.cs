using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Helpers
{
    public class AddUserForElectionFilter
    {
        public static string CreateQueryString(ObjectForUsersFilter objectForUsersFilter)
        {
            string firstPart = "SELECT IdUser,Username,FirstName,LastName,NrMatricol,Email,IsAccountActive FROM Users ";                                 

            string secondPart = "";


            if (objectForUsersFilter.IdRole != -1)
            {
                firstPart+= "INNER JOIN Users_Roles on Users.IdUser = Users_Roles.IdUser ";
                secondPart += " AND Roles.IdRole=" + objectForUsersFilter.IdRole.ToString();
            }

            if (objectForUsersFilter.IdDepartment != -1)
            {
                firstPart += "INNER JOIN Users_Departments on Users.IdUser = Users_Departments.IdUser ";
                secondPart += " AND Departments.IdDepartment=" + objectForUsersFilter.IdDepartment.ToString();

                if (objectForUsersFilter.IdCollege != -1)
                {
                    secondPart += " AND  Colleges.IdCollege=" + objectForUsersFilter.IdCollege.ToString();
                }
            }
            else 
            {
                if (objectForUsersFilter.IdCollege != -1)
                {
                    firstPart += "INNER JOIN Users_Departments on Users.IdUser = Users_Departments.IdUser ";
                    secondPart += " AND  Colleges.IdCollege=" + objectForUsersFilter.IdCollege.ToString();
                }
            }

            firstPart += " WHERE ";

            return firstPart + secondPart.Substring(5);
        }

        public static IList<User> GetFilteredUsersForElection(ObjectForUsersFilter objectForUsersFilter)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-RPNBQ1M;Integrated Security=true;Database=OnlineVotingApp;"))
            {
                con.Open();

                List<User> users = new List<User>();

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

                        if(!users.Exists(u=>u.IdUser == IdUser))
                        {
                            users.Add(new User(IdUser, Username, FirstName, LastName, NrMatricol, Email, IsAccountActive));
                        }
                       
                    }
                }

                con.Close();

                return users;

            }
        }

    }
}
