namespace Bot.Messages.ChartMessages;

public class LinksToChartsMessage : BaseMessage
{
    private readonly string _prefix = "https://a15687-8a2e.g.d-f.pw/";
    
    public override string MessageText => "Чтобы посмотреть график, выбери продукт.";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Лимонная кислота", url: $"{_prefix}lemonAcid"),
            InlineKeyboardButton.WithUrl(text: "Яблочная кислота", url : $"{_prefix}malicAcid")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Янтарная кислота", url : $"{_prefix}succinicAcid"),
            InlineKeyboardButton.WithUrl(text: "Винная кислота", url : $"{_prefix}tartaricAcid")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Соль «Экстра»", url : $"{_prefix}salt"),
            InlineKeyboardButton.WithUrl(text: "Хлорид калия", url : $"{_prefix}potassiumChloride")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Пропиленгликоль", url : $"{_prefix}propyleneGlycol"),
            InlineKeyboardButton.WithUrl(text: "Глутамат натрия", url : $"{_prefix}monosodiumGlutamate")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Бензоат натрия", url : $"{_prefix}sodiumBenzoate"),
            InlineKeyboardButton.WithUrl(text: "Сорбат калия", url : $"{_prefix}potassiumSorbate")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Полисорбат-80", url : $"{_prefix}polysorbate80"),
            InlineKeyboardButton.WithUrl(text: "🧴 Купи-флакон 🧴", url : $"{_prefix}kupiFlakon"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"fGoBack")
        },
    });
}
