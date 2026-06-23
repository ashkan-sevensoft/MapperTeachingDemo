using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Domain.Students.Dto;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Business.Students;
public class StudentMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateStudentDto, Student>();



        config.NewConfig<Student, StudentDetailListDto>()
            .Map(d => d.FullName, s => $"{s.FirstName} {s.LastName}");




        config.NewConfig<Student , AddStudentResultDto>()
              .Map(d => d.FullName, s => $"{s.FirstName} {s.LastName}");


    }
}
