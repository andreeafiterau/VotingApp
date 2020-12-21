using VotingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace VotingApp.Helpers
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }

        public DbSet <User> Users { get; set; }

        public DbSet <Candidate> Candidates { get; set; }

        public DbSet <Electoral_Room> ElectoralRooms { get; set; }

        public DbSet <Role> Roles { get; set; }

        public DbSet <Vote> Votes { get; set; }

        public DbSet <User_Role> Users_Roles { get; set; }

        public DbSet<User_Department> Users_Departments { get; set; }

        public DbSet<Election_User> Election_Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<College> Colleges { get; set; }

        public DbSet<Activation_Code> Activation_Codes { get; set; }

        public DbSet<Candidate_Electoral_Room> Candidate_Electoral_Rooms { get; set; }



    }
}
