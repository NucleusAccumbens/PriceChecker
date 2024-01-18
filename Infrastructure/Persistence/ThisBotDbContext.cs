using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public class ThisBotDbContext : DbContext, IBotDbContext
{
    private readonly string _connectionString =
        "Server=217.28.223.127,17160;User Id=user_d890c;Password=Ty4%_7Cfx*9A2Y;Database=db_41f02;;TrustServerCertificate=True";
    
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<TlgUser> TlgUsers => Set<TlgUser>();

    public DbSet<StoingPrice> StoingPrices => Set<StoingPrice>();

    public DbSet<KupiFlakonPrice> KupiFlakonPrices => Set<KupiFlakonPrice>();

    public ThisBotDbContext(DbContextOptions<ThisBotDbContext> options, 
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public ThisBotDbContext() 
    { 

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        builder.Entity<StoingPrice>()
            .Property(p => p.ProductPrice)
            .HasColumnType("decimal(18,2)");

        builder.Entity<KupiFlakonPrice>()
            .Property(p => p.ProductPrice)
            .HasColumnType("decimal(18,2)");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        optionsBuilder
            .UseSqlServer(_connectionString);

    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
