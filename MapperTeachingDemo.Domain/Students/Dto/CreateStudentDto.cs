using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Domain.Students.Dto;
public class CreateStudentDto
{
    [MaxLength(50)]
    public string FirstName { get;   set; }
   
    [MaxLength(50)]
    public string LastName { get;  set; }

    [EmailAddress]
    public string Email { get;  set; }

    public DateTime BirthDate { get;  set; }

}
