using Bot.Messages.ChartMessages;
using Bot.Messages.GeneralMessages;

namespace Bot.Commands.ChartCommands;

public class ChartMenuCallbackCommand : BaseCallbackCommand
{
    private readonly ChoicePeriodMessage _choicePeriodMessage = new();
    
    public override char CallbackDataCode => 'e';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _choicePeriodMessage.EditMessage(chatId, messageId, client);
        }
    }
}
