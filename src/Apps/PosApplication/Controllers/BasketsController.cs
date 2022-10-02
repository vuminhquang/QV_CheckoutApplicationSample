using FreeBot.Crosscutting.Exceptions;
using FreeBot.Web.Rest.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PosApplication.Application.Abstraction.Commands.Basket;
using PosApplication.Application.Abstraction.Queries.Basket;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.Commands.Basket;
using PosApplication.Application.Queries.Basket;
using PosApplication.Domain.Dtos;
using PosApplication.Domain.Entities;
using PosApplication.Shared.Exceptions;
using PosApplication.WebHelpers.Extensions;
using PosApplication.WebHelpers.Rest.Utilities;

namespace PosApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly ILogger<BasketController> _logger;
    private const string EntityName = "basket";
 
    public BasketController(IBasketService basketService, ILogger<BasketController> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult<Basket>> CreateBasket([FromBody] Basket basket)
    {
        _logger.LogDebug("REST request to save Basket : {@Basket}", basket);
        if (basket.Id != 0)
            throw new BadRequestAlertException("A new basket cannot already have an ID", EntityName, "idexists");
        
        basket = await _basketService.CreateBasket(basket);
        
        return CreatedAtAction(nameof(RetrieveBasket), new { id = basket.Id }, basket)
            .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, basket.Id.ToString()));
    }

    [HttpPost("{id}/article-line")]
    public async Task<IActionResult> AddArticle(long id, [FromBody] Article article)
    {
        _logger.LogDebug("REST request to save Article : {@Article}", article);
        if (article.Id != 0)
            throw new BadRequestAlertException("A new article cannot already have an ID", EntityName, "idexists");
        // article.BasketId = id;
        var basket = await _basketService.AddArticle(id, article);
        return Ok(basket)
            .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, basket.Id.ToString()));;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> RetrieveBasket([FromRoute] long id)
    {
        _logger.LogDebug("REST request to retrieve Basket : {@Id}", id);
        var result = await _basketService.RetrieveBasket(id);
        return ActionResultUtil.WrapOrNotFound(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBasketStatus(long id, [FromBody] Status status)
    {
        _logger.LogDebug("REST request to change Basket status : {@Id}", id);
        var basket = await _basketService.UpdateBasketStatus(id, status);
        return Ok(basket)
            .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, basket.Id.ToString()));;
    }
}