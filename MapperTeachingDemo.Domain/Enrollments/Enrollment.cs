using MapperTeachingDemo.Domain.Courses;
using MapperTeachingDemo.Domain.Students;

namespace MapperTeachingDemo.Domain.Enrollments;

public class Enrollment : BaseEntity
{
    public Enrollment()
    {
    }

    public Enrollment(Guid studentId, Guid courseId, double? grade = null)
    {
        StudentId = studentId;
        CourseId = courseId;
        EnrollDate = DateTime.UtcNow;
        Grade = grade;
        Validate();
    }

    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = default!;

    public Guid CourseId { get; private set; }
    public Course Course { get; private set; } = default!;

    public DateTime EnrollDate { get; private set; }
    public double? Grade { get; private set; }

    public void SetGrade(double grade)
    {
        Grade = grade;
        ModifiedAt = DateTime.UtcNow;
    }

    public override void Validate()
    {
        if (StudentId == Guid.Empty)
            throw new ArgumentException("StudentId cannot be empty.", nameof(StudentId));

        if (CourseId == Guid.Empty)
            throw new ArgumentException("CourseId cannot be empty.", nameof(CourseId));
    }
}
