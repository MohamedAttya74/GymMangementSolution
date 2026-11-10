using GymMangement.Configurations;
using GymMangement.Models;
using Microsoft.EntityFrameworkCore;

namespace GymMangement.DbContexts
{
    public class GymDbContext : DbContext
    {


        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GymManagementDb;Trusted_Connection=True;TrustServerCertificate=true;");
        }


        public DbSet<Plan> Plans { get; set; }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
        }


    }
}
