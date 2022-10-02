using System.Diagnostics;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Domain.Dtos;
using PosApplication.Domain.Entities;
using PosApplication.Test.Fixtures;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace PosApplication.Test;

public class BasketServiceUnitTest : TestBed<BasketServiceFixture>
{
  public BasketServiceUnitTest(ITestOutputHelper testOutputHelper, BasketServiceFixture fixture) : base(
    testOutputHelper, fixture)
  {
  }

  [Fact]
  public async Task AddNewBasket_WhenGetBasketById_ThenReceivedSameBasket()
  {
    #region Test Inputs

    var json = @"
{
  ""id"": 0,
  ""articles"": [
    {
      ""article"": ""tomato"",
      ""price"": 20
    },
    {
      ""article"": ""juice"",
      ""price"": 10
    }
  ],
  ""totalNet"": 30,
  ""totalGross"": 33,
  ""customer"": ""Andrei"",
  ""paysVAT"": true
}
    ";

    #endregion

    var services = _fixture.GetServiceProvider(_testOutputHelper);

    var basketService = services.GetRequiredService<IBasketService>();
    var basket = JsonSerializer.Deserialize<Basket>(json);
    await basketService.CreateBasket(basket);

    basket.Articles.Should().HaveCount(2);
    basket.TotalNet.Should().Be(30);
    basket.TotalGross.Should().Be(33);
    basket.Customer.Should().Be("Andrei");
    basket.PaysVAT.Should().BeTrue();
    basket.Status.Should().Be("open");
  }

  [Fact]
  public async Task AddNewArticleToBasket_WhenGetBasketById_ThenReceived3SameArticles()
  {
    #region Test Inputs

    var basketJson = @"
{
  ""id"": 0,
  ""articles"": [
    {
      ""article"": ""juice"",
      ""price"": 20
    },
    {
      ""article"": ""tomato"",
      ""price"": 10
    }
  ],
  ""totalNet"": 30,
  ""totalGross"": 33,
  ""customer"": ""Bob"",
  ""paysVAT"": true
}
    ";
    var articleJson = @"{ ""article"": ""water melon"", ""price"": 50 }";

    #endregion

    var services = _fixture.GetServiceProvider(_testOutputHelper);

    var basketService = services.GetRequiredService<IBasketService>();
    var basket = JsonSerializer.Deserialize<Basket>(basketJson);
    var returnedBasket = await basketService.CreateBasket(basket);
    var article = JsonSerializer.Deserialize<Article>(articleJson);
    Debug.Assert(article != null, nameof(article) + " != null");
    returnedBasket = await basketService.AddArticle(returnedBasket.Id, article);
    var retrievedBasket = await basketService.RetrieveBasket(returnedBasket.Id);
    retrievedBasket.Articles.Should().HaveCount(3);
    retrievedBasket.Articles[0].Name.Should().Be("juice");
    retrievedBasket.Articles[1].Name.Should().Be("tomato");
    retrievedBasket.Articles[2].Name.Should().Be("water melon");
    retrievedBasket.Articles[0].Price.Should().Be(20m);
    retrievedBasket.Articles[1].Price.Should().Be(10m);
    retrievedBasket.Articles[2].Price.Should().Be(50m);
    retrievedBasket.TotalNet.Should().Be(80m);
    retrievedBasket.TotalGross.Should().Be(88m);
    retrievedBasket.Customer.Should().Be("Bob");
    retrievedBasket.PaysVAT.Should().BeTrue();
    retrievedBasket.Status.Should().Be("open");
  }

  [Fact]
  public async Task GivenBasketHaveOpenStatus_WhenUpdateStatus_ThenReceivedCorrectStatus()
  {
    #region Test Inputs

    var basketJson = @"
{
  ""id"": 0,
  ""articles"": [
    {
      ""article"": ""water melon"",
      ""price"": 200
    },
    {
      ""article"": ""juice"",
      ""price"": 15
    }
  ],
  ""totalNet"": 0,
  ""totalGross"": 0,
  ""customer"": ""Alice"",
  ""paysVAT"": true
}
    ";

    var statusJson = @"{ ""status"":""closed"" }";

    #endregion

    var services = _fixture.GetServiceProvider(_testOutputHelper);

    var basketService = services.GetRequiredService<IBasketService>();
    var basket = JsonSerializer.Deserialize<Basket>(basketJson);
    var status = JsonSerializer.Deserialize<Status>(statusJson);
    Debug.Assert(basket != null, nameof(basket) + " != null");
    basket.TotalNet.Should().Be(0);
    basket.TotalGross.Should().Be(0);
    var returnedBasket = await basketService.CreateBasket(basket);
    var retrievedBasket = await basketService.RetrieveBasket(returnedBasket.Id);
    retrievedBasket.Articles.Should().HaveCount(2);
    retrievedBasket.Articles[0].Name.Should().Be("water melon");
    retrievedBasket.Articles[1].Name.Should().Be("juice");
    retrievedBasket.Articles[0].Price.Should().Be(200m);
    retrievedBasket.Articles[1].Price.Should().Be(15m);
    retrievedBasket.TotalNet.Should().Be(215m);
    retrievedBasket.TotalGross.Should().Be(236.5m);
    retrievedBasket.Customer.Should().Be("Alice");
    retrievedBasket.PaysVAT.Should().BeTrue();
    retrievedBasket.Status.Should().Be("open");
    Debug.Assert(status != null, nameof(status) + " != null");
    await basketService.UpdateBasketStatus(returnedBasket.Id, status);
    retrievedBasket = await basketService.RetrieveBasket(returnedBasket.Id);
    retrievedBasket.Status.Should().Be("closed");
  }

  [Fact]
  public async Task GivenBasket_WhenAddArticle_ThenTotalsAreCorrect()
  {
    #region Test Inputs

    var basketJson = @"
{
  ""id"": 0,
  ""articles"": [
    {
      ""article"": ""water melon"",
      ""price"": 200
    },
    {
      ""article"": ""juice"",
      ""price"": 15
    }
  ],
  ""totalNet"": 0,
  ""totalGross"": 0,
  ""customer"": ""Alice"",
  ""paysVAT"": true
}
    ";

    var articleJson = @"{ ""article"": ""orange"", ""price"": 52 }";

    #endregion

    var services = _fixture.GetServiceProvider(_testOutputHelper);

    var basketService = services.GetRequiredService<IBasketService>();
    var basket = JsonSerializer.Deserialize<Basket>(basketJson);

    Debug.Assert(basket != null, nameof(basket) + " != null");
    basket.TotalNet.Should().Be(0);
    basket.TotalGross.Should().Be(0);
    var returnedBasket = await basketService.CreateBasket(basket);
    var article = JsonSerializer.Deserialize<Article>(articleJson);
    Debug.Assert(article != null, nameof(article) + " != null");
    returnedBasket = await basketService.AddArticle(returnedBasket.Id, article);
    var retrievedBasket = await basketService.RetrieveBasket(returnedBasket.Id);
    retrievedBasket.Articles.Should().HaveCount(3);
    retrievedBasket.Articles[0].Name.Should().Be("water melon");
    retrievedBasket.Articles[1].Name.Should().Be("juice");
    retrievedBasket.Articles[2].Name.Should().Be("orange");
    retrievedBasket.Articles[0].Price.Should().Be(200m);
    retrievedBasket.Articles[1].Price.Should().Be(15m);
    retrievedBasket.Articles[2].Price.Should().Be(52m);
    retrievedBasket.TotalNet.Should().Be(267);
    retrievedBasket.TotalGross.Should().Be(293.7m);
    retrievedBasket.Customer.Should().Be("Alice");
    retrievedBasket.PaysVAT.Should().BeTrue();
    retrievedBasket.Status.Should().Be("open");
  }
}