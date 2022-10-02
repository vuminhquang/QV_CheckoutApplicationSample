using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PosApplication.Application.Abstraction.Commands.Basket;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.Commands.Basket;
using PosApplication.Application.Services;
using PosApplication.Domain.Entities;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace PosApplication.Test.Fixtures;

public class BasketServiceFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        services.AddMediatR(typeof(PosApplication.Application.DependencyRegister));
        services
            .AddScoped<IBasketService, BasketService>();
        services.AddTransient<IRequestHandler<BasketCreateCommand, Basket>, BasketCreateCommandHandler>();
        var infraDependencyRegister = new PosApplication.Infrastructure.DependencyRegister();
        infraDependencyRegister.ServicesRegister(services);
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new TestAppSettings {Filename = "appsettings.json", IsOptional = false};
    }

    protected override ValueTask DisposeAsyncCore()
        => new();
}