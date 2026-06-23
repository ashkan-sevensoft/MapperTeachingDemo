using MapperTeachingDemo.Domain.Enrollments;

namespace MapperTeachingDemo.Domain.Courses;

public class Course : BaseEntity
{
    public Course()
    {
    }

    public Course(string title, string instructor, int credit)
    {
        Title = title;
        Instructor = instructor;
        Credit = credit;
        Validate();
    }

    public string Title { get; private set; } = default!;
    public string Instructor { get; private set; } = default!;
    public int Credit { get; private set; }

    public List<Enrollment> Enrollments { get; private set; } = new();

    public void UpdateInfo(string title, string instructor, int credit)
    {
        Title = title;
        Instructor = instructor;
        Credit = credit;
        Validate();
        ModifiedAt = DateTime.UtcNow;
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentNullException(nameof(Title), "Title cannot be null or whitespace.");

        if (Credit <= 0)
            throw new ArgumentOutOfRangeException(nameof(Credit), "Credit must be greater than zero.");
    }
}
