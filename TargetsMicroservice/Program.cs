using Microsoft.EntityFrameworkCore;
using TargetsMicroservice;
using TargetsMicroservice.Repositories.Implementations;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Services.Implementations;
using TargetsMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MagisterkaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL")));

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightService, FlightService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
