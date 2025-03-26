using Microsoft.EntityFrameworkCore;
using Minio;
using RabbitMQ.Client;
using TargetsMicroservice;
using TargetsMicroservice.Repositories.Implementations;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Services.Implementations;
using TargetsMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

ConnectionFactory factory = new ConnectionFactory();
factory.UserName = configuration["RabbitMQ:UserName"];
factory.Password = configuration["RabbitMQ:Password"];
factory.HostName = configuration["RabbitMQ:HostName"];
factory.ClientProvidedName = configuration["RabbitMQ:ClientProvidedName"];

var queuesSection = builder.Configuration.GetSection("RabbitMQ:Queues");

string TARGETS_TARGETS_QUEUE = queuesSection["TargetsTargets"];
string TARGETS_FLIGHT_BEGIN_QUEUE = queuesSection["TargetsFlightBegin"];
string TARGETS_FLIGHT_END_QUEUE = queuesSection["TargetsFlightEnd"];


IConnection conn = await factory.CreateConnectionAsync();
IChannel channel = await conn.CreateChannelAsync();

string BUCKET_NAME = configuration["Minio:BucketName"];
string MINIO_URI = configuration["Minio:MinioUri"];
string MINIO_SERVER = configuration["Minio:Server"];
string MINIO_LOGIN = configuration["Minio:Login"];
string MINIO_PASSWORD = configuration["Minio:Password"];

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<MagisterkaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL")));

builder.Services.AddSingleton<IMinioClient>(serviceProvider =>
{
    return new MinioClient()
        .WithEndpoint(MINIO_SERVER)
        .WithCredentials(MINIO_LOGIN, MINIO_PASSWORD)
        .WithSSL(false) 
        .Build();
});

builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<IFlightsService, FlightsService>();
builder.Services.AddScoped<ITargetTypesRepository, TargetTypeRepository>();
builder.Services.AddScoped<ITargetTypesService, TargetTypeService>();
builder.Services.AddScoped<ITargetsRepository, TargetsRepository>();
builder.Services.AddScoped<ITargetsService, TargetsService>();
builder.Services.AddScoped<ICrucialPlacesRepository, CrucialPlacesRepository>();
builder.Services.AddScoped<ICrucialPlacesService, CrucialPlacesService>();
builder.Services.AddScoped<IReconAreasRepository, ReconAreasRepository>();
builder.Services.AddScoped<IReconAreasService, ReconAreasService >();
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
builder.Services.AddHostedService<RabbitMQTargetConsumerService>(sp =>
{
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitMQTargetConsumerService(channel, TARGETS_TARGETS_QUEUE, serviceScopeFactory);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
