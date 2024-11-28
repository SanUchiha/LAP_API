using Microsoft.EntityFrameworkCore;
using SimpleLAP.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LapDbSimpleContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

const string Cors = "Cors";
builder.Services.AddCors(options => //Add Cors Polity
{
    options.AddPolicy(Cors, app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Cors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
