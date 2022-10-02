using MediatR;
using PosApplication.Application.Abstraction.Queries.Basket;
using PosApplication.Domain.Repositories;

namespace PosApplication.Application.Queries.Basket
{
    public class BasketGetQueryHandler : IRequestHandler<BasketGetQuery, Domain.Entities.Basket>
    {
        private readonly IReadOnlyBasketRepository _basketRepository;

        public BasketGetQueryHandler(
            IReadOnlyBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Domain.Entities.Basket> Handle(BasketGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _basketRepository.QueryHelper()
                .Include(basket =>  basket.Articles)
                .GetOneAsync(basket => basket.Id == request.Id);
            return entity;
        }
    }
}
