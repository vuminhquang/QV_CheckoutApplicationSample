using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.WithEvent.Commands.Basket;

namespace PosApplication.Application.WithEvent.Kafka;

public class MessageConsumer: BackgroundService
{
    private readonly ILogger<MessageConsumer> _log;
    private readonly IConsumer<string, string> _consumer;
    private readonly IBasketService _basketService;

    public MessageConsumer(ILogger<MessageConsumer> log, IConsumer<string, string> consumer, IBasketService basketService)
    {
        _log = log;
        _consumer = consumer;
        _basketService = basketService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                
                if (consumeResult.Message.Key == "CreateTopicOnly") continue;
                
                var command = JsonSerializer.Deserialize<BasketKafkaCreateCommand>(consumeResult.Message.Value);
                var basket = await _basketService.CreateBasket(command.Basket);
                _log.LogInformation("Created Basket id {BasketId} from Command Guid:{CommandGuid}", basket.Id, command.Guid);
                
                _consumer.Commit();
            }
            catch (Exception e)
            {
                _log.LogError("BackgroundService failed {Message} ", e.Message);
                break;
            }
        }
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}