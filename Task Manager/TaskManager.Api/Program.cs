using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Application.Mgrs;
using TaskManager.Domain.Models;
using TaskManager.EF;
using TaskManager.Application;
using TaskManager.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

//builder.Services.AddControllers()
//    .AddJsonOptions(x =>
//    {
//        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    });

// Register the DbContext with EF Core and SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<Employee>), typeof(GenericRepository<Employee>));
builder.Services.AddScoped(typeof(IEmployeeMgr), typeof(EmployeeMgr));

builder.Services.AddScoped(typeof(IGenericRepository<Department>), typeof(GenericRepository<Department>));
builder.Services.AddScoped(typeof(IDepartmentMgr), typeof(DepartmentMgr));

builder.Services.AddScoped(typeof(IGenericRepository<TaskManager.Domain.Models.Task>), typeof(GenericRepository<TaskManager.Domain.Models.Task>));
builder.Services.AddScoped(typeof(ITaskMgr), typeof(TaskMgr));

builder.Services.AddScoped(typeof(IGenericRepository<TaskAssignment>), typeof(GenericRepository<TaskAssignment>));
builder.Services.AddScoped(typeof(ITaskAssignmentMgr), typeof(TaskAssignmentMgr));

builder.Services.AddScoped(typeof(IGenericRepository<TimeSlice>), typeof(GenericRepository<TimeSlice>));
builder.Services.AddScoped(typeof(ITimeSliceMgr), typeof(TimeSliceMgr));

builder.Services.AddAutoMapper(typeof(Mapping));


// Register MVC-style API controllers
builder.Services.AddControllers();

// CORS policy to allow frontend at 127.0.0.1:5500
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Enable Swagger/OpenAPI for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger only in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "";
    });
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable authorization middleware (used for secure endpoints)
app.UseAuthorization();

app.UseCors("AllowFrontend");

// Map controller endpoints (e.g., GET /api/tasks)
app.MapControllers();

// Run the web app
app.Run();