using GymMangement.DAL.Data.Configurations;
using GymMangement.DAL.Data.Models;
//using GymMangement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymMangement.DAL.Data.DbContexts
{
    public class GymDbContext : DbContext
    {

        public GymDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }


    }
}
