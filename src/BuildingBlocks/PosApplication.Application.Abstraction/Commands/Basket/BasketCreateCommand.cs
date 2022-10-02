using MediatR;

namespace PosApplication.Application.Abstraction.Commands.Basket
{
    public class BasketCreateCommand : IRequest<Domain.Entities.Basket>
    {
        public Domain.Entities.Basket Basket { get; init; }
    }
}
