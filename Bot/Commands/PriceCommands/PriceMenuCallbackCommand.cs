using Bot.Messages.PriceMessages;

namespace Bot.Commands.PriceCommands;

public class PriceMenuCallbackCommand : BaseCallbackCommand
{
    private readonly PriceMenuMessage _priceMenuMessage = new();

    public override char CallbackDataCode => 'a';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _priceMenuMessage.EditMessage(chatId, messageId, client);
        }
    }
}
