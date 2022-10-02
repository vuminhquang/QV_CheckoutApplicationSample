using MediatR;
using PosApplication.Application.Abstraction.Commands.Basket;
using PosApplication.Application.Abstraction.Queries.Basket;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.WithEvent.Commands.Basket;
using PosApplication.Domain.Dtos;
using PosApplication.Domain.Entities;
using PosApplication.Shared.Exceptions;

namespace PosApplication.Application.Services;

public class BasketService : IBasketService
{
    private readonly IMediator _mediator;
    private const string EntityName = "basket";

    public BasketService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Basket> CreateBasket(Basket basket)
    {
        UpdateBasketTotals(basket);
        EnsuringFields(basket);
        basket = await _mediator.Send(new BasketCreateCommand {Basket = basket});
        return basket;
    }
    
    public async Task CreateBasketKafka(string guid, Basket basket)
    {
        UpdateBasketTotals(basket);
        EnsuringFields(basket);
        await _mediator.Send(new BasketKafkaCreateCommand {Guid = guid, Basket = basket});
    }

    public async Task<Basket> AddArticle(long id, Article article)
    {
        var basket = await _mediator.Send(new BasketGetQuery {Id = id});
        if (basket == null)
            throw new EntityNotFoundException("Entity not found", EntityName, "enf");
        basket.Articles.Add(article);
        UpdateBasketTotals(basket);
        basket = await _mediator.Send(new BasketUpdateCommand {Basket = basket});
        return basket;
    }
    
    public async Task<Basket> RetrieveBasket(long id)
    {
        var result = await _mediator.Send(new BasketGetQuery {Id = id});
        return result;
    }
    
    public async Task<Basket> UpdateBasketStatus(long id, Status status)
    {
        var basket = await _mediator.Send(new BasketGetQuery {Id = id});
        if (basket == null)
            throw new EntityNotFoundException("Entity not found", EntityName, "enf");
        basket.Status = status.Name;
        basket = await _mediator.Send(new BasketUpdateCommand {Basket = basket});
        return basket;
    }
    
    private static void UpdateBasketTotals(Basket basket)
    {
        basket.TotalNet = 0;
        basket.TotalGross = 0;
        foreach (var article in basket.Articles)
        {
            basket.TotalNet += article.Price;
        }

        var vat = basket.PaysVAT ? 1.1m : 1m;
        basket.TotalGross = basket.TotalNet * vat;
    }
    
    private static void EnsuringFields(Basket basket)
    {
        if (string.IsNullOrEmpty(basket.Status))
        {
            basket.Status = "open";
        }
    }
}