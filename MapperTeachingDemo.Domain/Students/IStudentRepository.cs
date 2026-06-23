namespace MapperTeachingDemo.Domain.Students;

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<Student?> GetByEmailAsync(string email);
    Task<Student?> GetWithEnrollmentsAsync(Guid id);
}
