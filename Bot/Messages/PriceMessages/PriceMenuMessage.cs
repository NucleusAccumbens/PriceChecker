namespace Bot.Messages.PriceMessages;

public class PriceMenuMessage : BaseMessage
{
    public override string MessageText => "Выбери компанию.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "СТОИНГ", callbackData: "bStoing"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "КУПИ-ФЛАКОН", callbackData: "bBottle")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"^")
        },
    });
}
