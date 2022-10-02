using EngineFramework;
using FreeBot.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PosApplication.Domain.Repositories;
using PosApplication.Infrastructure.Repositories;
using RepositoryHelper;

namespace PosApplication.Infrastructure;

public class DependencyRegister : IDependencyRegister
{
    public void ServicesRegister(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseInMemoryDatabase("InMemoryDb");
        });
        services.AddScoped<DbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IReadOnlyBasketRepository, ReadOnlyBasketRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}