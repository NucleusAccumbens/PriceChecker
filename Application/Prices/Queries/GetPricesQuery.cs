using Application.Prices.Interfaces;

namespace Application.Prices.Queries;

public class GetPricesQuery : IGetPricesQuery
{
    private readonly IBotDbContext _context;

    public GetPricesQuery(IBotDbContext context)
    {
        _context = context;
    }

    public async Task<KupiFlakonPrice?> GetKupiFlakonPrice(Product productName, string amount)
    {
        var prices = await _context.KupiFlakonPrices
           .Where(price => price.Product == productName)
           .Where(price => price.Amount == amount)
           .OrderBy(price => price.CreatedAt)
           .ToListAsync();

        if (prices != null && prices.Count > 0)
        {
            return prices.Last();
        }

        else return null;
    }

    public async Task<List<KupiFlakonPrice>> GetKupiFlakonPrices(Product productName)
    {
        var prices = await _context.KupiFlakonPrices
            .Where(price => price.Product == productName)
            .Where(price => price.CreatedAt.Date == DateTime.UtcNow.Date)
            .OrderBy(price => price.CreatedAt)
            .ToListAsync();

        return prices;
    }

    public async Task<StoingPrice?> GetStoingPrice(Product productName, string amount)
    {
        var prices = await _context.StoingPrices
            .Where(price => price.Product == productName)
            .Where(price => price.Amount == amount)
            .OrderBy(price => price.CreatedAt)
            .ToListAsync();

        if (prices != null && prices.Count > 0)
        {
            return prices.Last();
        }

        else return null;
    }

    public async Task<List<StoingPrice>?> GetStoingPrices(Product productName)
    {
        var prices = await _context.StoingPrices
            .Where(price => price.Product == productName)
            .Where(price => price.CreatedAt.Date == DateTime.UtcNow.Date)
            .OrderBy(price => price.CreatedAt)
            .ToListAsync();

        return prices;
    }

    public async Task<List<StoingPrice>?> GetMaxStoingPricesForPeriod(Product productName, int daysCount)
    {
        var prices = await _context.StoingPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        if (prices != null)
        {
            var maxPrices = new List<StoingPrice>();
            
            DateTime date = DateTime.UtcNow.Date - TimeSpan.FromDays(daysCount - 1);

            while (date.Date != DateTime.UtcNow.Date.AddDays(1))
            {
                var prc = prices.Where(p => p.CreatedAt.ToShortDateString() == date.ToShortDateString())
                    .MaxBy(p => p.ProductPrice);

                if (prc != null) maxPrices.Add(prc);

                date += TimeSpan.FromDays(1);
            }

            return maxPrices;
        }

        else return null;
    }

    public async Task<List<KupiFlakonPrice>?> GetMaxKupiFlakonPricesForPeriod(Product productName, int daysCount)
    {
        var prices = await _context.KupiFlakonPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        if (prices != null)
        {
            var maxPrices = new List<KupiFlakonPrice>();

            DateTime date = DateTime.UtcNow.Date - TimeSpan.FromDays(daysCount - 1);

            while (date.Date != DateTime.UtcNow.Date.AddDays(1))
            {
                var prc = prices.Where(p => p.CreatedAt.ToShortDateString() == date.ToShortDateString())
                    .MaxBy(p => p.ProductPrice);

                if (prc != null) maxPrices.Add(prc);

                date += TimeSpan.FromDays(1);
            }

            return maxPrices;
        }

        else return null;
    }

    public async Task<string[]> GetAllStoingPriceDates(Product productName)
    {
        var prices = await _context.StoingPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        List<string> dates = new();

        for (int i = 0; i < prices.Count; i++)
        {
            string price = prices[i].CreatedAt.ToShortDateString();

            if (i == 0)
            {
                dates.Add(price);
            }

            if (i > 0 && !dates.Contains(price))
            {
                dates.Add(price);
            }  
        }

        return dates.ToArray();
    }

    public async Task<string[]> GetAllKupiFlakonPriceDates(Product productName)
    {
        var prices = await _context.KupiFlakonPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        List<string> dates = new();

        for (int i = 0; i < prices.Count; i++)
        {
            string price = prices[i].CreatedAt.ToShortDateString();

            if (i == 0)
            {
                dates.Add(price);
            }

            if (i > 0 && !dates.Contains(price))
            {
                dates.Add(price);
            }
        }

        return dates.ToArray();
    }

    public async Task<List<StoingPrice>?> GetMaxStoingPrices(Product productName)
    {
        var prices = await _context.StoingPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        if (prices != null)
        {
            var maxPrices = new List<StoingPrice>();

            DateTime date = prices.First().CreatedAt;

            while (date.Date != DateTime.UtcNow.Date.AddDays(1))
            {
                var prc = prices.Where(p => p.CreatedAt.ToShortDateString() == date.ToShortDateString())
                    .MaxBy(p => p.ProductPrice);

                if (prc != null) maxPrices.Add(prc);

                date += TimeSpan.FromDays(1);
            }

            return maxPrices;
        }

        else return null;
    }

    public async Task<List<KupiFlakonPrice>?> GetMaxKupiFlakonPrices(Product productName)
    {
        var prices = await _context.KupiFlakonPrices
            .Where(p => p.Product == productName)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        if (prices != null)
        {
            var maxPrices = new List<KupiFlakonPrice>();

            DateTime date = prices.First().CreatedAt;

            while (date.Date != DateTime.UtcNow.Date.AddDays(1))
            {
                var prc = prices.Where(p => p.CreatedAt.ToShortDateString() == date.ToShortDateString())
                    .MaxBy(p => p.ProductPrice);

                if (prc != null) maxPrices.Add(prc);

                date += TimeSpan.FromDays(1);
            }

            return maxPrices;
        }

        else return null;
    }
}
