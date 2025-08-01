using Calculator.Data;
using Calculator.Repository;
using Calculator.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CalculatorDbContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryDb")));

builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddScoped<ICalculatorRepository, CalculatorRepository>();

builder.Logging.AddConsole();

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
