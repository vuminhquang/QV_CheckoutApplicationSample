using MediatR;
using RepositoryHelper.Abstraction.Pagination;

namespace PosApplication.Application.Abstraction.Queries.Basket
{
    public class BasketGetAllQuery : IRequest<IPage<Domain.Entities.Basket>>
    {
        public IPageable Page { get; set; }
    }
}
