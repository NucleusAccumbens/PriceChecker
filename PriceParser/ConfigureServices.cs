using PriceParser;
using PriceParser.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection AddPriceParserServices(this IServiceCollection services)
    {
        services.AddScoped<IPriceChecker, PriceChecker>();
        
        return services;
    }
}
