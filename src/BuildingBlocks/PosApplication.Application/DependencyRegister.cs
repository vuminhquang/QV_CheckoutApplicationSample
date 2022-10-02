using EngineFramework;
using Microsoft.Extensions.DependencyInjection;
using PosApplication.Application.Abstraction.Services;
using PosApplication.Application.Services;

namespace PosApplication.Application;

public class DependencyRegister : IDependencyRegister
{
    public void ServicesRegister(IServiceCollection services)
    {
        services.AddScoped<IBasketService, BasketService>();
    }
}