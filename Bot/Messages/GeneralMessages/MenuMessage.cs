using Bot.Common.Abstractions;

namespace Bot.Messages.GeneralMessages;

public class MenuMessage : BaseMessage
{
    public override string MessageText => "Выбери раздел.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "💰 Цены", callbackData: "a")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📈 Графики", callbackData: "e")
        },
    });
}
