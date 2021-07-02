using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Services.Queries
{
    public class VoteCount
    {

        public static int Count_Votes(int IdCandidate)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Server = DESKTOP-RPNBQ1M; Integrated Security = true; Database = VotingApp; ");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT COUNT(IdUser) FROM Votes WHERE IdCandidate=" + IdCandidate.ToString();
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            int voteCount = (int)cmd.ExecuteScalar();

            sqlConnection1.Close();

            return voteCount;
        }

    }
}
