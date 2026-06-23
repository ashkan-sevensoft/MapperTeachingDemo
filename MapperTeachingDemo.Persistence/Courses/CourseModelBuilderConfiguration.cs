using MapperTeachingDemo.Domain.Courses;
using MapperTeachingDemo.Persistence.SeedData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapperTeachingDemo.Persistence.Courses;

public class CourseModelBuilderConfiguration : BaseModelBuilderConfiguration<Course>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Instructor).HasMaxLength(150).IsRequired();

        builder.HasData(
            new
            {
                Id = SeedIds.CourseDotnet,
                Title = ".NET Web API",
                Instructor = "Ali Darvish",
                Credit = 3,
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.CourseSql,
                Title = "SQL Server",
                Instructor = "Mona Yousefi",
                Credit = 2,
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.CourseDesignPatterns,
                Title = "Design Patterns",
                Instructor = "Ali Darvish",
                Credit = 3,
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            }
        );
    }
}
