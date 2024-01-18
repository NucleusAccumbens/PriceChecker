using Domain.Entities;

namespace Application.Prices.Interfaces;

public interface ICreatePriceCommand
{
    Task CreateStoingPriceAsync(StoingPrice price);

    Task CreateFlakonPriceAsync(KupiFlakonPrice price);
}
