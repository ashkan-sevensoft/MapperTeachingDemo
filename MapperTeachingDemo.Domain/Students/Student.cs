using MapperTeachingDemo.Domain.Enrollments;

namespace MapperTeachingDemo.Domain.Students;

public class Student : BaseEntity
{
    public Student()
    {
    }

    public Student(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        Validate();
    }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public DateTime BirthDate { get; private set; }

    public List<Enrollment> Enrollments { get; private set; } = new();

    public void UpdateInfo(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Validate();
        ModifiedAt = DateTime.UtcNow;
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentNullException(nameof(FirstName), "FirstName cannot be null or whitespace.");

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@'))
            throw new ArgumentException("Email is not valid.", nameof(Email));
    }
}
