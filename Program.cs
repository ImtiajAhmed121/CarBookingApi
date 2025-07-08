using CarBookingApi.Data;
using CarBookingApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

builder.Services.AddDbContext<WafiDbContext>(options =>
    options.UseInMemoryDatabase("CarBookingDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", 
        policy => policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WafiDbContext>();
    if (!context.Cars.Any())
    {
        context.Cars.AddRange(
            new Car { Name = "Toyota" },
            new Car { Name = "Tesla" },
            new Car { Name = "Honda" }
        );
        context.SaveChanges();
    }
}

app.Run();
