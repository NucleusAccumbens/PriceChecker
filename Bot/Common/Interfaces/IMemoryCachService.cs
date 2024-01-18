using Domain.Enums;

namespace Bot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(long chatId, string commandState);

    void SetMemoryCach(long chatId, int trysCount);

    void SetMemoryCach(int peroiod);

    string? GetCommandStateFromMemoryCach(long chatId);

    int GetTrysCountFromMemoryCach(long chatId);

    int GetPeriodFromMemorycach();
}
