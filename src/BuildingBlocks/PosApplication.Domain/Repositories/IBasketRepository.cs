using PosApplication.Domain.Entities;
using RepositoryHelper.Abstraction;

namespace PosApplication.Domain.Repositories
{
    public interface IBasketRepository : IGenericRepository<Basket, long>
    {
    }
}
