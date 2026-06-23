using MapperTeachingDemo.Domain;
using MapperTeachingDemo.Domain.Courses;
using MapperTeachingDemo.Domain.Enrollments;
using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Persistence.Courses;
using MapperTeachingDemo.Persistence.Enrollments;
using MapperTeachingDemo.Persistence.Students;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Persistence;
public static class PersistanceServiceEx
{
    public static   IServiceCollection AddSPersistanceServices(
        this IServiceCollection services 
        )
    {
      

         services.AddScoped<IStudentRepository, StudentRepository>();
         services.AddScoped<ICourseRepository, CourseRepository>();
         services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;

    }

}
