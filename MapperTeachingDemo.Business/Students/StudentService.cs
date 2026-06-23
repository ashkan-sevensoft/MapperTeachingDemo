using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Domain.Students.Dto;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Business.Students;
public class StudentService : IstudentService
{
    private readonly IStudentRepository _studentRepository;

    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository , IMapper mapper)
    {
       _studentRepository = studentRepository;
        _mapper = mapper;
    }
    public async Task<AddStudentResultDto> CreateAsync(CreateStudentDto dto , CancellationToken cancellation)
    {

        var student = _mapper.Map<Student>(dto); 
        await _studentRepository.AddAsync(student, cancellation);
        return _mapper.Map<AddStudentResultDto>(student); ;
    }

    public async Task<AddStudentResultDto> CreateWithManualMappAsync(CreateStudentDto dto,CancellationToken cancellation)
    {
       var student = new Student(dto.FirstName , dto.LastName ,dto.Email , dto.BirthDate);
        await _studentRepository.AddAsync(student,cancellation);
        return new AddStudentResultDto
        {
            Id=student.Id,
            FullName=student.FirstName+" "+dto.LastName
        };


    }

    public async Task<List<StudentDetailListDto>> GetAllAsync()
    {
        var students = await _studentRepository.QueryAsync(s => true);

        return _mapper.Map<List<StudentDetailListDto>>(students);
    }
}
