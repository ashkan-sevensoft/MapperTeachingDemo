using MapperTeachingDemo.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace MapperTeachingDemo.Persistence.Students;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Student?> GetByEmailAsync(string email)
    {
        return await DbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<Student?> GetWithEnrollmentsAsync(Guid id)
    {
        return await DbContext.Students
            .AsNoTracking()
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
