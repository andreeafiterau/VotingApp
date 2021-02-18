using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Helpers
{
    public class AddToKeylessTable
    {
        public static void AddToTable_User_Role(int IdUser, int IdRole)
        {
            SqlConnection sqlConnection1 =new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Users_Roles (IdUser, IdRole) VALUES ("+IdUser.ToString()+","+ IdRole.ToString()+")";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        public static void AddToTable_User_Department(int IdUser, int IdDepartment)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Users_Departments (IdUser, IdDepartment) VALUES (" + IdUser.ToString() + "," + IdDepartment.ToString() + ")";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        public static void AddToTable_Vote(int IdUser, int IdCandidate)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Votes (IdUser, IdCandidate) VALUES (" + IdUser.ToString() + "," + IdCandidate.ToString() + ")";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        public static void UpdateTable_User_Role(int IdUser,int IdRole)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Users_Roles " +
                             "SET IdUser=" + IdUser.ToString() + ",IdRole=" + IdRole.ToString()
                              + " WHERE IdUser=" + IdUser.ToString() + ";";

            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        //trebuie sa trimit prin params si departamentul vechi
        public static void UpdateTable_User_Department(int IdUser, int IdDepartment)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Users_Departments " +
                              "SET IdUser=" + IdUser.ToString() + ",IdDepartment=" + IdDepartment.ToString()
                              + " WHERE IdUser=" + IdUser.ToString() + ";";

            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        public static void AddToTable_Election_Users(int IdElectoralRoom,int IdUser)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Elections_Users (IdUser, IdElectoralRoom) VALUES (" + IdUser.ToString() + "," + IdElectoralRoom.ToString() + ")";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        //public static void DeleteFromTable_Users_Roles(int idUser)
        //{
        //    SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = 
        //    cmd.Connection = sqlConnection1;

        //    sqlConnection1.Open();
        //    cmd.ExecuteNonQuery();
        //    sqlConnection1.Close();
        //}
    }
}
