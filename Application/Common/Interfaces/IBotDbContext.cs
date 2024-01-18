namespace Application.Common.Interfaces;

public interface IBotDbContext : IDisposable
{
    DbSet<TlgUser> TlgUsers { get; }

    DbSet<StoingPrice> StoingPrices { get; }

    DbSet<KupiFlakonPrice> KupiFlakonPrices { get; }
    
    Task SaveChangesAsync();
}
