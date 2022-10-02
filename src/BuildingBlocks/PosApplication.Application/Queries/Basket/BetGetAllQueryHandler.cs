using MediatR;
using PosApplication.Application.Abstraction.Queries.Basket;
using PosApplication.Domain.Repositories;
using RepositoryHelper.Abstraction.Pagination;

namespace PosApplication.Application.Queries.Basket;

public class BasketGetAllQueryHandler : IRequestHandler<BasketGetAllQuery, IPage<Domain.Entities.Basket>>
{
    private IReadOnlyBasketRepository _basketRepository;

    public BasketGetAllQueryHandler(IReadOnlyBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<IPage<Domain.Entities.Basket>> Handle(BasketGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _basketRepository.QueryHelper()
            .Include(basket => basket.Articles)
            .GetPageAsync(request.Page);
        return page;
    }
}