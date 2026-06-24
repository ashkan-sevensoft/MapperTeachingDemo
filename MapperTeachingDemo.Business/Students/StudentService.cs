using MapperTeachingDemo.Domain;
using MapperTeachingDemo.Domain.Enrollments;
using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Domain.Students.Dto;
using MapperTeachingDemo.WebAPI.Cashing;
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

    private readonly IUnitOfWork _unitOfWork;

    private readonly ICacheService _cacheService;

    public StudentService(IStudentRepository studentRepository,
                         IMapper mapper,
                         IUnitOfWork unitOfWork,
                         ICacheService cacheService)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cacheService=cacheService;
    }
    public async Task<AddStudentResultDto> CreateAsync(CreateStudentDto dto, CancellationToken cancellation)
    {

        //var student = _mapper.Map<Student>(dto);
        //await _studentRepository.AddAsync(student, cancellation);
        //return _mapper.Map<AddStudentResultDto>(student); 

        var student = _mapper.Map<Student>(dto);
        await _unitOfWork.Repository<Student>().AddAsync(student,cancellation);
        await _unitOfWork.SaveChangesAsync(cancellation);

        return _mapper.Map<AddStudentResultDto>(student);

    }

    public async Task<AddStudentResultDto> CreateWithManualMappAsync(CreateStudentDto dto, CancellationToken cancellation)
    {
        var student = new Student(dto.FirstName, dto.LastName, dto.Email, dto.BirthDate);
        await _studentRepository.AddAsync(student, cancellation);
        return new AddStudentResultDto
        {
            Id=student.Id,
            FullName=student.FirstName+" "+dto.LastName
        };


    }

    public async Task<List<StudentDetailListDto>> GetAllAsync(bool fromCache = true)
    {
        var cacheKey = "student:allData";
        if (fromCache)
        {

            var cached = await _cacheService.Get<List<StudentDetailListDto>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }

        }
        var students = await _studentRepository.QueryAsync(s => true);
        var result =  _mapper.Map<List<StudentDetailListDto>>(students);
        await _cacheService.Set(cacheKey, result,TimeSpan.FromMinutes(10));

        return result;

    }
}
