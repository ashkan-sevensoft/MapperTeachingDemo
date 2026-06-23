using MapperTeachingDemo.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace MapperTeachingDemo.Persistence.Courses;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Course?> GetWithEnrollmentsAsync(Guid id)
    {
        return await DbContext.Courses
            .AsNoTracking()
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
