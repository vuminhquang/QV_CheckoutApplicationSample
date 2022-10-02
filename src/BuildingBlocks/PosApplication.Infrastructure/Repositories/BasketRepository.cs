using FreeBot.Domain.Repositories.Interfaces;
using PosApplication.Domain.Entities;
using PosApplication.Domain.Repositories;
using RepositoryHelper;

namespace PosApplication.Infrastructure.Repositories;

public class BasketRepository : GenericRepository<Basket, long>, IBasketRepository
{
    public BasketRepository(IUnitOfWork context) : base(context)
    {
    }

    public override async Task<Basket> CreateOrUpdateAsync(Basket bet)
    {
        //Create or Update included Articles also
        var entitiesToBeUpdated = new List<Type>{typeof(Article)};
        return await base.CreateOrUpdateAsync(bet, entitiesToBeUpdated);
    }
}