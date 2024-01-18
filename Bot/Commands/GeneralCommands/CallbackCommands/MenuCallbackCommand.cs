using Bot.Common.Abstractions;
using Bot.Messages.GeneralMessages;

namespace Bot.Commands.GeneralCommands.CallbackCommand;

public class MenuCallbackCommand : BaseCallbackCommand
{
    private readonly MenuMessage _menuMessage = new();

    public override char CallbackDataCode => '^';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _menuMessage.EditMessage(chatId, messageId, client);
        }
    }
}
