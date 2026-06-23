using MapperTeachingDemo.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace MapperTeachingDemo.Persistence.Enrollments;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Enrollment>> GetByStudentIdAsync(Guid studentId)
    {
        return await DbContext.Enrollments
            .AsNoTracking()
            .Include(e => e.Course)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();
    }
}
