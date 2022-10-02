using MediatR;

namespace PosApplication.Application.WithEvent.Commands.Basket
{
    public class BasketKafkaCreateCommand : IRequest<Unit>
    {
        public string Guid { get; init; }
        public Domain.Entities.Basket Basket { get; init; }
    }
}
