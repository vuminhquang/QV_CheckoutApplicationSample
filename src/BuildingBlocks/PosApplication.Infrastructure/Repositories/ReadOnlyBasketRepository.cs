using FreeBot.Domain.Repositories.Interfaces;
using PosApplication.Domain.Entities;
using PosApplication.Domain.Repositories;
using RepositoryHelper;

namespace PosApplication.Infrastructure.Repositories
{
    public class ReadOnlyBasketRepository : ReadOnlyGenericRepository<Basket, long>, IReadOnlyBasketRepository
    {
        public ReadOnlyBasketRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
