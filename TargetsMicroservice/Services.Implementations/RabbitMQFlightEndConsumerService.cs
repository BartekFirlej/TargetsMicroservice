using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class RabbitMQFlightEndConsumerService : BackgroundService
    {
        private readonly IChannel _channel;
        private readonly string _FLIGHTS_FLIGHT_END_QUEUE;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQFlightEndConsumerService(IChannel channel, string FLIGHTS_FLIGHT_END_QUEUE, IServiceScopeFactory serviceScopeFactory)
        {
            _channel = channel;
            _FLIGHTS_FLIGHT_END_QUEUE = FLIGHTS_FLIGHT_END_QUEUE;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var _flightsService = scope.ServiceProvider.GetRequiredService<IFlightsService>();
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Queue {_FLIGHTS_FLIGHT_END_QUEUE}: {message}");
                        FlightEndRequest flightRequest = JsonSerializer.Deserialize<FlightEndRequest>(message);
                        await _flightsService.EndFlight(flightRequest);
                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing message from {_FLIGHTS_FLIGHT_END_QUEUE}: {ex}");
                    }
                }
            };

            await _channel.BasicConsumeAsync(queue: _FLIGHTS_FLIGHT_END_QUEUE,
                                             autoAck: true,
                                             consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }


        public override void Dispose()
        {
            _channel.CloseAsync();
            base.Dispose();
        }
    }
}
