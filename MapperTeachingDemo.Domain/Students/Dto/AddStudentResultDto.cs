using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Domain.Students.Dto;
public class AddStudentResultDto
{

    public  Guid Id { get; set; }

    public string FullName { get; set; }
}
