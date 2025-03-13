using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class RabbitMQTargetConsumerService : BackgroundService
    {
        private readonly IChannel _channel;
        private readonly string _TARGETS_TARGETS_QUEUE;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQTargetConsumerService(IChannel channel, string TARGETS_TARGETS_QUEUE, IServiceScopeFactory serviceScopeFactory)
        {
            _channel = channel;
            _TARGETS_TARGETS_QUEUE = TARGETS_TARGETS_QUEUE;
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
                        var _targetService = scope.ServiceProvider.GetRequiredService<ITargetsService>();
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Queue {_TARGETS_TARGETS_QUEUE}: {message}");
                        TargetRequest targetRequest = JsonSerializer.Deserialize<TargetRequest>(message);
                        await _targetService.AddTarget(targetRequest);
                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing message from {_TARGETS_TARGETS_QUEUE}: {ex}");
                    }
                }
            };

            await _channel.BasicConsumeAsync(queue: _TARGETS_TARGETS_QUEUE,
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
