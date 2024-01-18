using Bot.Messages.PriceMessages;

namespace Bot.Commands.PriceCommands;

public class ChoiceWebsiteCallbackCommand : BaseCallbackCommand
{
    private readonly StoingPriceMenuMessage _stoingPriceMenuMessage = new();

    private readonly KupiFlakonPriceMenuMessage _kupiFlakonPriceMenuMessage = new();

    private readonly PriceMenuMessage _priceMenuMessage = new();

    public override char CallbackDataCode => 'b';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            if (update.CallbackQuery.Data == "bStoing")
            {
                await _stoingPriceMenuMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "bBottle")
            {
                await _kupiFlakonPriceMenuMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "bGoBack")
            {
                await _priceMenuMessage.EditMessage(chatId, messageId, client);

                return;
            }
        }
    }
}
