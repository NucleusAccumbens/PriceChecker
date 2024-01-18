namespace Bot.Messages.PriceMessages;

public class StoingPriceMenuMessage : BaseMessage
{
    public override string MessageText => "Выбери продукт, цену которого хочешь узнать.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Лимонная кислота", callbackData: "cLemonAcid"),
            InlineKeyboardButton.WithCallbackData(text: "Яблочная кислота", callbackData: "cMalicAcid")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Янтарная кислота", callbackData: "cSuccinicAcid"),
            InlineKeyboardButton.WithCallbackData(text: "Винная кислота", callbackData: "cTartaricAcid")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Соль «Экстра»", callbackData: "cSalt"),
            InlineKeyboardButton.WithCallbackData(text: "Хлорид калия", callbackData: "cPotassiumChloride")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Пропиленгликоль", callbackData: "cPropyleneGlycol"),
            InlineKeyboardButton.WithCallbackData(text: "Глутамат натрия", callbackData: "cMonosodiumGlutamate")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Бензоат натрия", callbackData: "cSodiumBenzoate"),
            InlineKeyboardButton.WithCallbackData(text: "Сорбат калия", callbackData: "cPotassiumSorbate")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Полисорбат-80", callbackData: "cPolysorbate80"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"bGoBack")
        },
    });
}
