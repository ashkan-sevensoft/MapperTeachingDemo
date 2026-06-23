using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Domain.Students.Dto;
public class StudentDetailListDto
{ 
    
   
    public string FullName { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

}
