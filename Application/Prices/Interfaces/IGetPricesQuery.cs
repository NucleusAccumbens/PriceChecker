namespace Application.Prices.Interfaces;

public interface IGetPricesQuery
{
    Task<StoingPrice?> GetStoingPrice(Product productName, string amount);

    Task<List<StoingPrice>?> GetStoingPrices(Product productName);

    Task<List<StoingPrice>?> GetMaxStoingPricesForPeriod(Product productName, int daysCount);

    Task<KupiFlakonPrice?> GetKupiFlakonPrice(Product productName, string amount);

    Task<List<KupiFlakonPrice>> GetKupiFlakonPrices(Product productName);

    Task<List<KupiFlakonPrice>?> GetMaxKupiFlakonPricesForPeriod(Product productName, int daysCount);

    Task<string[]> GetAllStoingPriceDates(Product productName);

    Task<string[]> GetAllKupiFlakonPriceDates(Product productName);

    Task<List<StoingPrice>?> GetMaxStoingPrices(Product productName);

    Task<List<KupiFlakonPrice>?> GetMaxKupiFlakonPrices(Product productName);
}
