using MapperTeachingDemo.Domain.Enrollments;
using MapperTeachingDemo.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapperTeachingDemo.Persistence.Enrollments;

public class EnrollmentModelBuilderConfiguration : BaseModelBuilderConfiguration<Enrollment>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasOne(x => x.Student)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Course)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new
            {
                Id = SeedIds.Enrollment1,
                StudentId = SeedIds.Student1,
                CourseId = SeedIds.CourseDotnet,
                EnrollDate = new DateTime(2026, 2, 1),
                Grade = 18.5,
                CreatedAt = new DateTime(2026, 2, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.Enrollment2,
                StudentId = SeedIds.Student1,
                CourseId = SeedIds.CourseSql,
                EnrollDate = new DateTime(2026, 2, 1),
                Grade = (double?)null,
                CreatedAt = new DateTime(2026, 2, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.Enrollment3,
                StudentId = SeedIds.Student2,
                CourseId = SeedIds.CourseDotnet,
                EnrollDate = new DateTime(2026, 2, 5),
                Grade = 19.0,
                CreatedAt = new DateTime(2026, 2, 5),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.Enrollment4,
                StudentId = SeedIds.Student3,
                CourseId = SeedIds.CourseDesignPatterns,
                EnrollDate = new DateTime(2026, 2, 10),
                Grade = 17.25,
                CreatedAt = new DateTime(2026, 2, 10),
                IsDeleted = false
            }
        );
    }
}
