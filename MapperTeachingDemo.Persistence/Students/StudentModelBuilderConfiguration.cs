using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Persistence.SeedData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapperTeachingDemo.Persistence.Students;

public class StudentModelBuilderConfiguration : BaseModelBuilderConfiguration<Student>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Student> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasData(
            new
            {
                Id = SeedIds.Student1,
                FirstName = "Ali",
                LastName = "Rezaei",
                Email = "ali.rezaei@example.com",
                BirthDate = new DateTime(2000, 3, 21),
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.Student2,
                FirstName = "Sara",
                LastName = "Ahmadi",
                Email = "sara.ahmadi@example.com",
                BirthDate = new DateTime(2001, 7, 10),
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            },
            new
            {
                Id = SeedIds.Student3,
                FirstName = "Reza",
                LastName = "Karimi",
                Email = "reza.karimi@example.com",
                BirthDate = new DateTime(1999, 11, 2),
                CreatedAt = new DateTime(2026, 1, 1),
                IsDeleted = false
            }
        );
    }
}
