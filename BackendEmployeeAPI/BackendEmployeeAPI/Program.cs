

using BackendEmployeeAPI.Models;
using BackendEmployeeAPI.Services.Contract;
using BackendEmployeeAPI.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using BackendEmployeeAPI.Utilites;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbemployeeContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddCors(option => option.AddPolicy("PolicyToAPP",
    app =>
    {
        app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PolicyToAPP");
app.UseAuthorization();

app.MapControllers();

app.Run();
