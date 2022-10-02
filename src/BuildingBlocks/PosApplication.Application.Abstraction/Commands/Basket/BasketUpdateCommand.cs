using MediatR;

namespace PosApplication.Application.Abstraction.Commands.Basket
{
    public class BasketUpdateCommand : IRequest<Domain.Entities.Basket>
    {
        public Domain.Entities.Basket Basket { get; set; }
    }
}
