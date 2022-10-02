using PosApplication.Domain.Dtos;
using PosApplication.Domain.Entities;

namespace PosApplication.Application.Abstraction.Services;

public interface IBasketService
{
    Task<Basket> CreateBasket(Basket basket);
    Task<Basket> AddArticle(long id, Article article);
    Task<Basket> RetrieveBasket(long id);
    Task<Basket> UpdateBasketStatus(long id, Status status);
}