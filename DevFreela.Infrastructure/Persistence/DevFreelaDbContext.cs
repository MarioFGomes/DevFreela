using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext:DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<ProjectComment> ProjectComents { get; set; }

        public DbSet<UserSkill> UsersSkills { get; set; }

        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options):base(options) {
        
              
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      
        }



    }

  
}
