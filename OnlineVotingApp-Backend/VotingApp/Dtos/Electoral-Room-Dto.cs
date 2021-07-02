using System;

namespace VotingApp.Dtos
{
    public class Electoral_Room_Dto
    {
        public int IdElectoralRoom { get; set; }

        public int IdElectionType { get; set; }

        public DateTime Date { get; set; }

        public int IdCollege { get; set; }

        public int IdDepartment { get; set; }
    }
}
