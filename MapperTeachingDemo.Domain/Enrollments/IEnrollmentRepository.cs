namespace MapperTeachingDemo.Domain.Enrollments;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    Task<List<Enrollment>> GetByStudentIdAsync(Guid studentId);
}
