using Bot.Messages.ChartMessages;

namespace Bot.Commands.ChartCommands;

public class ChoicePeriodCallbackCommand : BaseCallbackCommand
{
    private readonly LinksToChartsMessage _linksToChartsMessage = new();

    private readonly ChoicePeriodMessage _choicePeriodMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    public ChoicePeriodCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }
    
    public override char CallbackDataCode => 'f';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "fWeek")
            {
                if (await CheckDataAvailability(7, callbackId, client) == true)
                {
                    _memoryCachService.SetMemoryCach(7);

                    await _linksToChartsMessage.EditMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "fMonth")
            {
                if (await CheckDataAvailability(30, callbackId, client) == true)
                {
                    _memoryCachService.SetMemoryCach(30);

                    await _linksToChartsMessage.EditMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "fThreeMonth")
            {
                if (await CheckDataAvailability(90, callbackId, client) == true)
                {
                    _memoryCachService.SetMemoryCach(90);

                    await _linksToChartsMessage.EditMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "fSixMonth")
            {
                if (await CheckDataAvailability(180, callbackId, client) == true)
                {
                    _memoryCachService.SetMemoryCach(180);

                    await _linksToChartsMessage.EditMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "fYear")
            {
                if (await CheckDataAvailability(365, callbackId, client) == true)
                {
                    _memoryCachService.SetMemoryCach(365);

                    await _linksToChartsMessage.EditMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "fAll")
            {
                _memoryCachService.SetMemoryCach(0);

                await _linksToChartsMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "fGoBack")
            {
                await _choicePeriodMessage.EditMessage(chatId, messageId, client);
            }
        }
    }

    private async Task ShowAllert(string callbackId, ITelegramBotClient client)
    {
        await MessageService.ShowAllert(callbackId, client, "Данные за этот период ещё не собраны.");
    }

    private async Task<bool> CheckDataAvailability(int daysCount, string callbackId, ITelegramBotClient client)
    {
        DateTime x = DateTime.Today - TimeSpan.FromDays(daysCount);

        if (x < new DateTime(2022, 12, 12))
        {
            await ShowAllert(callbackId, client);

            return false;
        }

        return true;
    }
}
