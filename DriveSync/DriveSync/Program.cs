using DriveSync.BusinessLogic;
using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DBContext;
using DriveSync.Services;
using DriveSync.Services.IServices;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrateDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
