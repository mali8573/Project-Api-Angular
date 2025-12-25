using LotteryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LotteryDbContext>(options =>
//options.UseSqlServer("Server=Srv2\\pupils;Database=LotteryDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));

options.UseSqlServer("Server=DESKTOP-1L8084V\\SQLEXPRESS;Database=LotteryDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//    });
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
