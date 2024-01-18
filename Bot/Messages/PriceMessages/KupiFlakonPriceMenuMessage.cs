namespace Bot.Messages.PriceMessages;

public class KupiFlakonPriceMenuMessage : BaseMessage
{
    public override string MessageText => "Выбери продукт, цену которого хочешь узнать.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Флакон кор. (Китай)", callbackData: "dBottleBrown1")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Флакон кор. (Чехия)", callbackData: "dBottleBrown2")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Флакон чёрный", callbackData: "dBottleBlack")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Пипетка круглая", callbackData: "dPipetteRound"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Пипетка квадратная", callbackData: "dPipetteSq"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"bGoBack")
        },
    });
}
