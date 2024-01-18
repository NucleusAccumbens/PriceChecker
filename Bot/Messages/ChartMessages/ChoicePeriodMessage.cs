namespace Bot.Messages.ChartMessages;

public class ChoicePeriodMessage : BaseMessage
{
    public override string MessageText => "Выбери период.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "7 дней", callbackData: "fWeek"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "30 дней", callbackData: "fMonth")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "90 дней", callbackData: "fThreeMonth")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "180 дней", callbackData: "fSixMonth")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "365 дней", callbackData: "fYear")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Все данные", callbackData: "fAll")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "^")
        },
    });
}
