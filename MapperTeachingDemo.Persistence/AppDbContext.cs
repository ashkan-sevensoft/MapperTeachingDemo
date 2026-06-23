using System.Reflection;
using MapperTeachingDemo.Domain.Courses;
using MapperTeachingDemo.Domain.Enrollments;
using MapperTeachingDemo.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace MapperTeachingDemo.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
