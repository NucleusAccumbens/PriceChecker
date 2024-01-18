using Domain.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Bot.Common.Services;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId);

        if (result is not null and string)
        {
            return (string)result;
        }

        else return null;
    }

    public int GetTrysCountFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId + 1);

        if (result is not null and int)
        {
            return (int)result;
        }

        else return 0;
    }

    public int GetPeriodFromMemorycach()
    {
        var result = _memoryCach.Get(1);

        if (result is not null and int)
        {
            return (int)result;
        }

        else return 0;
    }

    public void SetMemoryCach(long chatId, string commandState)
    {
        _memoryCach.Set(chatId, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }

    public void SetMemoryCach(long chatId, int trysCount)
    {
        _memoryCach.Set(chatId + 1, trysCount,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }


    public void SetMemoryCach(int daysCount)
    {
        _memoryCach.Set(1, daysCount,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }   
}
