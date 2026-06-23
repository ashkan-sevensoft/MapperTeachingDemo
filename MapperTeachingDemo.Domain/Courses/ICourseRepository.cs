namespace MapperTeachingDemo.Domain.Courses;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course?> GetWithEnrollmentsAsync(Guid id);
}
