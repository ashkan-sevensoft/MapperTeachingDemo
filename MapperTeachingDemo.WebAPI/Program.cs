using MapperTeachingDemo.Business.Students;
using MapperTeachingDemo.Domain;
using MapperTeachingDemo.Domain.Courses;
using MapperTeachingDemo.Domain.Enrollments;
using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Persistence;
using MapperTeachingDemo.Persistence.Courses;
using MapperTeachingDemo.Persistence.Enrollments;
using MapperTeachingDemo.Persistence.Students;
using MapperTeachingDemo.WebAPI.Caching;
using MapperTeachingDemo.WebAPI.Cashing;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddSingleton<ICacheService, RedisCacheService>();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(StudentMapsterConfig).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MapperTeachingDemo API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddSPersistanceServices();


builder.Services.AddScoped<IstudentService, StudentService>();

 


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MapperTeachingDemo API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
