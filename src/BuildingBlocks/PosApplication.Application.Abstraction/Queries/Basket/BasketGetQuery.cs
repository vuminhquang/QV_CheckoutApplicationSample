using MediatR;

namespace PosApplication.Application.Abstraction.Queries.Basket
{
    public class BasketGetQuery : IRequest<Domain.Entities.Basket>
    {
        public long Id { get; set; }
    }
}
