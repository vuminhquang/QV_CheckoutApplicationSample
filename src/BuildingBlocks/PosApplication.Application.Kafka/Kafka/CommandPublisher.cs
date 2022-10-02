using Confluent.Kafka;
using MediatR;

namespace PosApplication.Application.WithEvent;

public class CommandPublisher : IMessagePublisher, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic;

    public CommandPublisher(IProducer<string, string> producer, string topic)
    {
        _producer = producer;
        _topic = topic;
    }
        
    // public async Task Publish(IReadOnlyCollection<Weather> weathers, CancellationToken cancellationToken)
    // {
    //     var tasks = weathers
    //         .Select(weather =>
    //         {
    //             var message = new Message<string, int>()
    //             {
    //                 Key = weather.City,
    //                 Value = weather.Temperature
    //             };
    //
    //             return _producer.ProduceAsync(_topic, message, cancellationToken);
    //         })
    //         .ToArray();
    //
    //     await Task.WhenAll(tasks);
    // }

    public void Dispose()
    {
        _producer.Dispose();
    }

    public async Task Publish(string command, CancellationToken cancellationToken)
    {
        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = command
        };
        await _producer.ProduceAsync(_topic, message, cancellationToken);
    }
}