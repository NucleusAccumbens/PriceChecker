using Bot.Common.Abstractions;

namespace Bot.Messages.GeneralMessages;

public class NewUserMessage : BaseMessage
{
    public override string MessageText => "Чтобы использовать бота, " +
        "отправь кодовое слово сообщением в этот чат.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => null;
}
