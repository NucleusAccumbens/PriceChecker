using Application.Common.EnumParsers;
using Application.Prices.Interfaces;
using Domain.Enums;

namespace Bot.Messages.PriceMessages;

internal class KupiFlakonPriceMessage : BaseMessage
{
    private readonly Product _product;

    private readonly IGetPricesQuery _getPricesQuery;

    private readonly string _postfix;

    public KupiFlakonPriceMessage(IGetPricesQuery getPricesQuery, Product product, string postfix)
    {
        _product = product;
        _getPricesQuery = getPricesQuery;
        _postfix = postfix;
    }

    public override string MessageText => GetMessage().Result;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private async Task<string> GetMessage()
    {
        string text = $"<b>{ProductEnumParser.GetProductStringValue(_product)}</b>";

        var prices = await _getPricesQuery.GetKupiFlakonPrices(_product);

        if (prices != null && prices.Count > 0)
        {
            if (prices[0].Availability == false) return text += $": <i>нет в наличии</i>";

            text += "\n\n";

            foreach (var price in prices)
            {

                if (price.ProductPrice != 0)
                {
                    text += $"Цена ({price.Amount}): <b>{price.ProductPrice} р./шт.</b>\n";
                }
            }
            text += "\n";

            return text;
        }

        return text;
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl(text: "Перейти на страницу товара",
                url: $"https://kupi-flakon.ru/{_postfix}")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "✖️ скрыть сообщение", callbackData: $"!")
            },
        });
    }
}
