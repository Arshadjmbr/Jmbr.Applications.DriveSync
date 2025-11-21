using DriveSync.BusinessLogic;
using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DBContext;
using DriveSync.ExceptionHandler;
using DriveSync.MiddleWare;
using DriveSync.Services;
using DriveSync.Services.IServices;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DriveSyncDbContext>(options => options.UseSqlServer
           (builder.Configuration.GetConnectionString("DbConnection")));

//Add Migrations
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
.AddFluentMigratorCore()
.ConfigureRunner(c => c
.AddSqlServer()
.WithGlobalConnectionString("DbConnection")
.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations().For.EmbeddedResources());

builder.Services.AddTransient<ISqlService, SqlService>();
builder.Services.AddTransient<IUserBl, UserBl>();
builder.Services.AddTransient<IUserBr, UserBr>();
builder.Services.AddTransient<ILoginBl, LoginBl>();
builder.Services.AddTransient<ILoginBr, LoginBr>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DriveSync", Version = "1.0.0" });
    c.AddSecurityDefinition(name: "DriveSync", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Scheme = "Bearer",
        In = ParameterLocation.Header

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "DriveSync"
            },
            Scheme = "DriveSync",
            Name = "DriveSync",
            In = ParameterLocation.Header
        },
        new List<string>()
    }
});
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseMiddleware<JwtTokenValidationMiddleWare>();

app.MigrateDatabase();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
