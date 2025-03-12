using Microsoft.EntityFrameworkCore;
using Minio;
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
const string BUCKET_NAME = "droneimages";
const string MINIO_URI = "http://localhost:9000";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MagisterkaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL")));

builder.Services.AddSingleton<IMinioClient>(serviceProvider =>
{
    return new MinioClient()
        .WithEndpoint("localhost:9000")
        .WithCredentials("minio", "minio123")
        .WithSSL(false) 
        .Build();
});

builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<IFlightsService, FlightsService>();
builder.Services.AddScoped<ITargetsRepository, TargetsRepository>();
builder.Services.AddScoped<ITargetsService, TargetsService>();
builder.Services.AddScoped<IPhotoUploadService>(sp =>
{
    var minio = sp.GetRequiredService<IMinioClient>();
    return new PhotoUploadService(minio, BUCKET_NAME, MINIO_URI);
});
builder.Services.AddHostedService<RabbitMQFlightBeginConsumerService>(sp =>
{
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitMQFlightBeginConsumerService(channel, TARGETS_FLIGHT_BEGIN_QUEUE, serviceScopeFactory);
});
builder.Services.AddHostedService<RabbitMQFlightEndConsumerService>(sp =>
{
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitMQFlightEndConsumerService(channel, TARGETS_FLIGHT_END_QUEUE, serviceScopeFactory);
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
