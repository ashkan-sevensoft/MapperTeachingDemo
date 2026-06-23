# اسکریپت تدریس - بخش ۱: توضیح پروژه + لایه سرویس

ترتیب باز کردن فایل‌ها دقیقاً همینه که نوشتم. هر بخش رو همینطوری که نوشتم بخون.

---

## ۱. باز کن: MapperTeachingDemo.Domain / BaseEntity.cs

بگو:
"این کلاس پایه‌ی همه‌ی Entity هاست. هر چیزی که توی دیتابیس ذخیره میشه، از این ارث می‌بره.

- `Id` از نوع Guid هست، نه int. چرا؟ چون توی پروژه‌های واقعی وقتی چند سرور یا چند دیتابیس داری، Guid تضمین می‌کنه که Id ها تکراری نشن، برخلاف int که AutoIncrement هست و فقط توی یه دیتابیس یونیکه.
- `CreatedAt` خودکار موقع ساخت ست میشه.
- `IsDeleted` و `DeletedAt` برای Soft Delete هستن — یعنی وقتی یوزر چیزی رو دیلیت می‌کنه، ما واقعاً از دیتابیس پاکش نمی‌کنیم، فقط فلگش رو می‌زنیم. این خیلی مهمه چون توی پروژه‌های واقعی نباید دیتا رو واقعاً از بین ببری (برای گزارش‌گیری، قانون، Audit و...).
- `SetAsDeleted()` متدیه که این فلگ رو ست می‌کنه، نه ست‌کننده‌ی public مستقیم — چون نمی‌خوایم هرجا بشه IsDeleted رو دستی true کرد، باید از طریق این متد کنترل‌شده انجام شه.
- `Validate()` که abstract هست، یعنی هر Entity باید قوانین خاص خودش رو پیاده کنه (مثلاً ایمیل نباید خالی باشه)."

نکته‌ی کلیدی که باید تاکید کنی: **همه‌ی Property ها `private set` دارن.** بگو:
"این عمداً اینطوریه. توی Encapsulation، نباید بشه از بیرون کلاس مقدار یه Property رو مستقیم عوض کرد. تغییر باید فقط از طریق متدهایی مثل `UpdateInfo` یا `SetAsDeleted` انجام شه، که داخلش می‌تونیم Validation بزنیم. اگه Property ها public set بودن، هرکسی می‌تونست بدون رعایت قانون‌ها مقدارشون رو عوض کنه."

---

## ۲. باز کن: MapperTeachingDemo.Domain / IGenericRepository.cs

بگو:
"این یه Interface جنریکه که عملیات پایه‌ی CRUD رو تعریف می‌کنه: Add، Query، GetById، Update، HardDelete (پاک کردن واقعی) و SoftDelete (فقط فلگ زدن).

چرا جنریک؟ چون این عملیات‌ها برای Student، Course، Enrollment و هر Entity دیگه‌ای دقیقاً یکسانه. به‌جای اینکه برای هر Entity از صفر بنویسیم، یه بار جنریک می‌نویسیم و همه از همینش استفاده می‌کنن.

ببین متد `QueryAsync` چطور `Expression<Func<TEntity, bool>>` می‌گیره — این یعنی می‌تونیم هر شرطی (Where) رو از بیرون بهش پاس بدیم، مثلاً `s => s.Email == "x@x.com"`."

---

## ۳. باز کن: MapperTeachingDemo.Domain / Students/Student.cs (و بعد Courses/Course.cs و Enrollments/Enrollment.cs)

بگو:
"این یه Entity واقعیه که از BaseEntity ارث‌بری کرده.

- دو Constructor داره: یکی پارامتر-خالی (که EF Core موقع خوندن از دیتابیس استفاده می‌کنه)، یکی با پارامتر (که موقع ساخت یه Student جدید توی کد استفاده می‌کنیم و داخلش Validate صدا زده میشه).
- `Enrollments` یه لیسته که رابطه‌ی این Student با Course هارو نشون میده — این یه رابطه‌ی Many-to-Many هست که از طریق Entity واسط Enrollment پیاده‌سازی شده (دقیقاً مثل رابطه‌ی User-Post-Like توی پروژه‌ی قبلی که درس دادیم).
- متد `UpdateInfo` تنها راهیه که میشه اطلاعات این Student رو عوض کرد، و داخلش دوباره Validate صدا زده میشه و ModifiedAt آپدیت میشه."

برای Course بگو: "دقیقاً همون الگو، با Title/Instructor/Credit."

برای Enrollment بگو:
"این Entity واسطه‌ست بین Student و Course. هر Enrollment یعنی یه دانشجو توی یه درس ثبت‌نام کرده، با تاریخ ثبت‌نام و نمره (Grade که Nullable هست چون شاید هنوز نمره نداده باشه)."

---

## ۴. باز کن: MapperTeachingDemo.Persistence / BaseModelBuilderConfiguration.cs

بگو:
"این کلاس Fluent API مشترک بین همه‌ی Entity هارو پیاده می‌کنه:
- Id به‌عنوان Primary Key
- ایندکس روی CreatedAt برای سرعت Query
- `HasQueryFilter(e => !e.IsDeleted)` — این خیلی مهمه! این یعنی EF Core خودکار به *همه‌ی* Query هایی که می‌زنیم، یه `WHERE IsDeleted = false` اضافه می‌کنه. ما هیچ‌جا توی کدمون لازم نیست خودمون بنویسیم `Where(x => !x.IsDeleted)` — خودکار اعمال میشه. این یعنی رکوردهای Soft-Delete شده اصلاً توی Query های عادی دیده نمیشن."

سپس برو سراغ یکی از فایل‌های Configuration واقعی، مثلاً `Students/StudentModelBuilderConfiguration.cs`، و بگو:
"اینجا قوانین خاص هر Entity رو می‌نویسیم: طول رشته‌ها (`HasMaxLength`)، Unique بودن Email، و در آخر `HasData(...)` که Seed Data ماست — یعنی وقتی Migration اجرا میشه، این رکوردها خودکار توی دیتابیس درج میشن. توجه کنید که برای Seed باید Id رو ثابت (از پیش تعیین‌شده) بدیم، چون نمی‌تونیم بگیم 'هر بار یه Guid رندوم جدید بساز' — این باید همیشه ثابت بمونه تا Migration ها Idempotent باشن."

---

## ۵. باز کن: MapperTeachingDemo.Persistence / GenericRepository.cs

بگو:
"این پیاده‌سازی IGenericRepository هست. نکته‌ی مهم: این کلاس `abstract` هست — یعنی نمیشه مستقیم ازش نمونه ساخت. باید یه کلاس مثل `StudentRepository` ازش ارث‌بری کنه. چرا abstract؟ چون می‌خوایم برای هر Entity یه Repository خاص خودش داشته باشیم (StudentRepository، CourseRepository) که علاوه بر متدهای جنریک، متدهای مخصوص خودش رو هم داشته باشه — مثلاً `GetByEmailAsync` که فقط برای Student معنی داره.

نکته‌ی دوم: پارامتر `tracking` توی متدها. وقتی `tracking = false` هست، از `AsNoTracking()` استفاده می‌کنیم که سریع‌تره و فقط برای Read مناسبه. وقتی می‌خوایم Update کنیم، باید `tracking = true` بدیم تا EF بتونه تغییرات رو دنبال کنه."

---

## ۶. باز کن: MapperTeachingDemo.Persistence / Students/StudentRepository.cs

بگو:
"این کلاس از GenericRepository ارث‌بری کرده و IStudentRepository رو implement می‌کنه. می‌بینید که فقط متدهای اضافه‌ی خاص Student رو نوشتیم (`GetByEmailAsync`, `GetWithEnrollmentsAsync`) — متدهای پایه (Add, Update, Delete...) رو از کلاس پدر گرفتیم، مجبور نبودیم دوباره بنویسیم. این دقیقاً همون مزیت Generic Repository + Inheritance هست."

برای `GetWithEnrollmentsAsync` بگو:
"اینجا با `Include` و `ThenInclude` داریم Eager Loading انجام می‌دیم — یعنی همراه با Student، لیست Enrollment هاش و Course هر Enrollment رو هم توی یه Query می‌گیریم، تا چندبار به دیتابیس Round-trip نزنیم."

---

## ۷. باز کن: MapperTeachingDemo.Persistence / AppDbContext.cs

بگو:
"این کلاس پل بین کد C# و دیتابیسه. هر DbSet یه جدول رو نشون میده. متد `OnModelCreating` با `ApplyConfigurationsFromAssembly` خودکار همه‌ی کلاس‌های Configuration (که از IEntityTypeConfiguration ارث‌بری کردن) رو توی همین اسمبلی پیدا می‌کنه و اعمال می‌کنه — بدون اینکه مجبور باشیم دستی یکی‌یکی صداشون بزنیم."

---

## ۸. باز کن: MapperTeachingDemo.WebAPI / Program.cs

بگو:
"اینجا همه‌چیز به هم وصل میشه (Dependency Injection):
- `AddDbContext` — DbContext رو با Connection String از appsettings.json رجیستر می‌کنیم.
- `AddScoped<IStudentRepository, StudentRepository>` — یعنی هرجا توی کد IStudentRepository بخوایم (مثلاً توی Constructor یه Controller یا Service)، خودکار یه نمونه از StudentRepository بهمون تزریق میشه. این یعنی کلاس‌های بالادست فقط Interface رو می‌شناسن، نه Implementation واقعی رو — این اصل Dependency Inversion هست.
- `Scoped` یعنی این Instance برای کل یه HTTP Request یکی می‌مونه، نه برای کل برنامه (Singleton) و نه هر بار جدید (Transient)."

و appsettings.json رو نشون بده، بگو: "ConnectionString اینجاست، جداگانه از کد، که اگه عوض شد لازم نباشه کد رو Recompile کنیم."

---

# بخش ۲: حالا میریم سراغ لایه‌ی Service (زنده تایپ کن)

اینجا قراره Business Layer رو بسازیم. هدف: کنترلر مستقیم با Repository کار نکنه، بلکه از طریق Service. توی همین لایه DTO ها رو هم تعریف می‌کنیم — که بعداً همینا میشن موضوع AutoMapper/Mapster.

### قدم ۱: DTO ها رو بساز

مسیر: `MapperTeachingDemo.Business/Students/Dtos/StudentDto.cs`

```csharp
namespace MapperTeachingDemo.Business.Students.Dtos;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime BirthDate { get; set; }
}
```

بگو وقتی این کد رو می‌نویسی:
"چرا DTO جدا از Entity؟ چند دلیل:
۱. Entity ما `private set` داره، نمی‌خوایم مستقیم سریالایزش کنیم و به کلاینت بدیم.
۲. شاید بخوایم فیلدهایی به کلاینت نشون بدیم که توی Entity نیست (مثل FullName که محاسبه‌شده‌ست) یا برعکس، بعضی فیلدها رو مخفی کنیم.
۳. اگه مستقیم Entity رو expose کنیم، هر تغییری توی دیتابیس مستقیم روی API ما هم اثر میگذاره — این باعث Coupling بد میشه."

مسیر: `MapperTeachingDemo.Business/Students/Dtos/CreateStudentDto.cs`

```csharp
namespace MapperTeachingDemo.Business.Students.Dtos;

public class CreateStudentDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime BirthDate { get; set; }
}
```

بگو: "این DTO مخصوص ورودیه، وقتی کلاینت می‌خواد یه Student جدید بسازه. متفاوته از StudentDto چون Id نباید از سمت کلاینت بیاد — ما خودمون می‌سازیمش."

### قدم ۲: Interface سرویس

مسیر: `MapperTeachingDemo.Business/Students/IStudentService.cs`

```csharp
using MapperTeachingDemo.Business.Students.Dtos;

namespace MapperTeachingDemo.Business.Students;

public interface IStudentService
{
    Task<StudentDto> CreateAsync(CreateStudentDto dto);
    Task<StudentDto?> GetByIdAsync(Guid id);
    Task<List<StudentDto>> GetAllAsync();
}
```

### قدم ۳: پیاده‌سازی سرویس با Manual Mapping (عمداً!)

مسیر: `MapperTeachingDemo.Business/Students/StudentService.cs`

```csharp
using MapperTeachingDemo.Business.Students.Dtos;
using MapperTeachingDemo.Domain.Students;

namespace MapperTeachingDemo.Business.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        var student = new Student(dto.FirstName, dto.LastName, dto.Email, dto.BirthDate);
        await _studentRepository.AddAsync(student);

        return new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            BirthDate = student.BirthDate
        };
    }

    public async Task<StudentDto?> GetByIdAsync(Guid id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null) return null;

        return new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            BirthDate = student.BirthDate
        };
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _studentRepository.QueryAsync(s => true, pageSize: 100);

        return students.Select(student => new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            BirthDate = student.BirthDate
        }).ToList();
    }
}
```

بعد از نوشتن این کد، بگو:
"حالا دقت کنید — توی هر متد، یه بلوک `new StudentDto { ... }` رو **دستی** کپی کردیم و تکرار کردیم. این همون **Manual Mapping** هست که موضوع اصلی امشبه.

مشکلاتش چیه؟
۱. تکرار کد (DRY نقض شده) — اگه یه Property به StudentDto اضافه شه، باید بریم سراغ همه‌ی این بلوک‌ها و دستی اضافه کنیم.
۲. اگه یادمون بره یه Property رو map کنیم، Compiler هیچ ارور نمیده، فقط Runtime یه فیلد خالی می‌مونه — باگ سخت برای پیدا کردن.
۳. هرچی Entity بزرگ‌تر و DTO ها بیشتر، این کد به‌شدت طولانی و کسل‌کننده میشه.

دقیقاً همینجا AutoMapper و Mapster وارد میشن — می‌خوایم همین Mapping رو با یه خط کد جایگزین کنیم."

### قدم ۴: Controller (برای تست با Swagger)

مسیر: `MapperTeachingDemo.WebAPI/Controllers/StudentsController.cs`

```csharp
using MapperTeachingDemo.Business.Students;
using MapperTeachingDemo.Business.Students.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapperTeachingDemo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var result = await _studentService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _studentService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _studentService.GetAllAsync());
    }
}
```

### قدم ۵: رجیستر کردن سرویس در Program.cs

این خط رو کنار خطوط Repository اضافه کن:

```csharp
builder.Services.AddScoped<IStudentService, MapperTeachingDemo.Business.Students.StudentService>();
```

بگو: "حالا با F5 برنامه رو اجرا می‌کنیم، توی Swagger میریم سراغ StudentsController، چندتا Request می‌زنیم، می‌بینیم Manual Mapping کار می‌کنه ولی چقدر کد تکراری نوشتیم. از همینجا شروع می‌کنیم به نصب AutoMapper."

---

## نکته‌ی پایانی برای خودت

بعد از اجرای موفق پروژه و دیدن کارکرد Manual Mapping توی Swagger، مستقیم برو سراغ:
1. نصب پکیج `AutoMapper` و نوشتن `Profile`
2. تبدیل `StudentService` به استفاده از `IMapper`
3. سپس همون کار رو با Mapster (`Adapt<T>()`) تکرار کن و کنار هم مقایسه کن.
