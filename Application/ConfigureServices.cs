using Application.Prices.Commands;
using Application.Prices.Interfaces;
using Application.Prices.Queries;
using Application.TlgUsers.Commands;
using Application.TlgUsers.Queries;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IKickTlgUserCommand, KickTlgUserCommand>();
        services.AddScoped<ICreatePriceCommand, CreatePriceCommand>();
        services.AddScoped<IGetPricesQuery, GetPricesQuery>();
        services.AddScoped<IGetTlgUsersQuery, GetTlgUsersQuery>();
        services.AddScoped<ICreateTlgUserCommand, CreateTlgUserCommand>();
        
        return services;
    }
}

