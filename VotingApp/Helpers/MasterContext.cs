using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace VotingApp.Helpers
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }

        public DbSet <User> Users { get; set; }

        public DbSet <Candidate> Candidates { get; set; }

        public DbSet <ElectoralRoom>ElectoralRooms { get; set; }

        public DbSet <ApplicationRole> ApplicationRoles { get; set; }
    }
}
