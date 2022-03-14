using FilmesAPIServer.Data;
using FilmesAPIServer.Repositories;
using FilmesAPIServer.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IFilmeService, FilmeService>();

builder.Services.AddDbContext<DataContext>
    (options => options.UseSqlServer
    ("Data Source=DESKTOP-R1GECP3;" +
    "Initial Catalog=FilmesAPI;" +
    "Integrated Security=true;" +
    "User Id=DESKTOP-R1GECP3\\Felipe;" +
    "Password=Secret;"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
