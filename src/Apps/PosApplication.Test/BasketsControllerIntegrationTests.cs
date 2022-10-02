using System.Net.Mime;
using System.Text;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PosApplication.Domain.Entities;
using PosApplication.Test.Extensions;

namespace PosApplication.Test
{
    public class BasketsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly IFixture _fixture;
        private readonly WebApplicationFactory<Program> _factory;

        public BasketsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _fixture = new Fixture();
        }

        // TEST NAME - CreateNewBasket
        // TEST DESCRIPTION - Create new basket should success, get back Basket by RetrieveBasket should success
        [Fact]
        public async Task CreateNewBasket()
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

            #endregion

            // Arrange
            var client = _factory
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            
            var url = $"/Basket";

            // Act
            var responseMessage = await client.PostAsync(url, new StringContent(basketJson, Encoding.UTF8, MediaTypeNames.Application.Json));

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            var basket = await responseMessage.DeserializeContentAsync<Basket>();
            basket.Articles.Should().HaveCount(2);
            basket.TotalNet.Should().Be(215);
            basket.TotalGross.Should().Be(236.5m);
            basket.Customer.Should().Be("Alice");
            basket.PaysVAT.Should().BeTrue();
            basket.Status.Should().Be("open");

            url = $"/Basket/{basket.Id}";
            responseMessage = await client.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            var retrievedBasket = await responseMessage.DeserializeContentAsync<Basket>();
            retrievedBasket.Articles.Should().HaveCount(2);
            retrievedBasket.TotalNet.Should().Be(215);
            retrievedBasket.TotalGross.Should().Be(236.5m);
            retrievedBasket.Customer.Should().Be("Alice");
            retrievedBasket.PaysVAT.Should().BeTrue();
            retrievedBasket.Status.Should().Be("open");
        }
    }
}
