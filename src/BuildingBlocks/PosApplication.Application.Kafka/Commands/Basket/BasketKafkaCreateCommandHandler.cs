using System.Text.Json;
using MediatR;
using PosApplication.Application.Abstraction.Commands.Basket;
using PosApplication.Domain.Repositories;

namespace PosApplication.Application.WithEvent.Commands.Basket;

public class BasketKafkaCreateCommandHandler : IRequestHandler<BasketKafkaCreateCommand>
{
    private readonly IMessagePublisher _messagePublisher;

    public BasketKafkaCreateCommandHandler(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task<Unit> Handle(BasketKafkaCreateCommand request, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(request);
        await _messagePublisher.Publish(json, cancellationToken);
        return default;
    }
}