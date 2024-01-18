using Application.Prices.Interfaces;

namespace Application.Prices.Commands;

public class CreatePriceCommand : ICreatePriceCommand
{
    private readonly IBotDbContext _context;

    public CreatePriceCommand(IBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateFlakonPriceAsync(KupiFlakonPrice price)
    {
        var prices = await _context.KupiFlakonPrices
            .Where(p => p.CreatedAt.Date == DateTime.UtcNow.Date)
            .Where(p => p.Amount == price.Amount)
            .Where(p => p.Product == price.Product)
            .ToListAsync();

        if (prices != null && prices.Count > 0)
        {
            foreach (var item in prices)
            {
                if (item.ProductPrice != price.ProductPrice && 
                    item.Amount == price.Amount &&
                    item.Product == price.Product)
                {
                    _context.KupiFlakonPrices.Remove(item);
                    
                    await _context.KupiFlakonPrices.AddAsync(price);

                    await _context.SaveChangesAsync();
                }
            }
        }
        else
        {
            await _context.KupiFlakonPrices.AddAsync(price);    

            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateStoingPriceAsync(StoingPrice price)
    {
        var prices = await _context.StoingPrices
            .Where(p => p.CreatedAt.Date == DateTime.UtcNow.Date)
            .Where(p => p.Amount == price.Amount)
            .Where(p => p.Product == price.Product)
            .ToListAsync();

        if (prices != null && prices.Count > 0)
        {
            foreach (var item in prices)
            {
                if (item.ProductPrice != price.ProductPrice && 
                    item.Product == price.Product)
                {
                    _context.StoingPrices.Remove(item);
                    
                    await _context.StoingPrices.AddAsync(price);

                    await _context.SaveChangesAsync();
                }
            }
        }
        else
        {
            await _context.StoingPrices.AddAsync(price);

            await _context.SaveChangesAsync();
        }
    }
}
