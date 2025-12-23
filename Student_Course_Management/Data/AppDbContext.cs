using Microsoft.EntityFrameworkCore;
using Student_Course_Management.Models;
namespace Student_Course_Management.Data
{
    public class AppDbContext:DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
            {
            }
            public DbSet<Student> Students { get; set; }
            public DbSet<Course> Courses { get; set; }
            public DbSet<Mapping> Mappings { get; set; }
            public DbSet<SpResult> SpResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpResult>().HasNoKey();

            modelBuilder.Entity<Course>().HasIndex(c => c.CourseCode).IsUnique();

            modelBuilder.Entity<Course>().Property(c => c.CourseCode).IsRequired();
        }
    }
}

