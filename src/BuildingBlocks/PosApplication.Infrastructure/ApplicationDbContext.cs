using Microsoft.EntityFrameworkCore;
using PosApplication.Domain.Entities;
// using PosApplication.Infrastructure.InMemoryDb;

namespace PosApplication.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Basket> Baskets => Set<Basket>();
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}