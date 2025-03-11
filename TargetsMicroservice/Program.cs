using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using TargetsMicroservice;
using TargetsMicroservice.Repositories.Implementations;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Services.Implementations;
using TargetsMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConnectionFactory factory = new ConnectionFactory();
factory.UserName = "guest";
factory.Password = "guest";
factory.HostName = "localhost";
factory.ClientProvidedName = "app:audit component:event-consumer";

IConnection conn = await factory.CreateConnectionAsync();
IChannel channel = await conn.CreateChannelAsync();

const string TARGETS_TARGETS_QUEUE = "Targets_Targets";
const string TARGETS_FLIGHT_BEGIN_QUEUE = "Targets_Flight_Begin";
const string TARGETS_FLIGHT_END_QUEUE = "Targets_Flight_End";


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MagisterkaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL")));

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightsService, FlightsService>();

builder.Services.AddHostedService<RabbitMQFlightBeginConsumerService>(sp =>
{
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitMQFlightBeginConsumerService(channel, TARGETS_FLIGHT_BEGIN_QUEUE, serviceScopeFactory);
});

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
