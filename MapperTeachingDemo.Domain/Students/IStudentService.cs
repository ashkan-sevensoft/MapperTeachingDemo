using MapperTeachingDemo.Domain.Students.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Domain.Students;
public interface IstudentService
{

    Task<AddStudentResultDto> CreateWithManualMappAsync(CreateStudentDto dto,CancellationToken cancellation);
    Task<AddStudentResultDto> CreateAsync(CreateStudentDto dto, CancellationToken cancellation);
    Task<List<StudentDetailListDto>> GetAllAsync(bool fromCache = true);

}
