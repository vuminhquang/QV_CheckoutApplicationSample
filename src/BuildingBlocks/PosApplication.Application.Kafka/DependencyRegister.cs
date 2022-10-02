using System.Text.Json;
using Confluent.Kafka;
using EngineFramework;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.WithEvent.Kafka;
using Serilog;

namespace PosApplication.Application.WithEvent;

public class DependencyRegister : IDependencyRegister
{
    public void ServicesRegister(IServiceCollection services)
    {
        var topicName = "CommandTopic";
        
        // Producer Config
        var producerConfig = new ProducerConfig { BootstrapServers = "localhost:9092"};
        
        var producer = new ProducerBuilder<string, string>(producerConfig).Build();
        producer.Produce(topicName, new Message<string, string>{Key = "CreateTopicOnly"});
                    
        services.AddSingleton<IMessagePublisher>(_ => new CommandPublisher(producer, topicName));

        // Consumer Config
        var consumerConfig = new ConsumerConfig
        {
            GroupId = "service-group",
            EnableAutoCommit = false,
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

        consumer.Subscribe(topicName);
        services.AddHostedService(sp =>
            new MessageConsumer(sp.GetRequiredService<ILogger<MessageConsumer>>(), consumer, sp.GetRequiredService<IBasketService>()));
    }
}