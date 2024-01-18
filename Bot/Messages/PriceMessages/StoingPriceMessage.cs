using Application.Common.EnumParsers;
using Application.Prices.Interfaces;
using Domain.Enums;

namespace Bot.Messages.PriceMessages;

public class StoingPriceMessage : BaseMessage
{
    private readonly Product _product;
    
    private readonly IGetPricesQuery _getPricesQuery;

    private readonly string _postfix;

    public StoingPriceMessage(IGetPricesQuery getPricesQuery, Product product, string postfix)
    {
        _getPricesQuery = getPricesQuery;
        _product = product;
        _postfix = postfix;
    }

    public override string MessageText => GetMessage().Result;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private async Task<string> GetMessage()
    {
        string text = $"<b>{ProductEnumParser.GetProductStringValue(_product)}</b>";
        
        var prices = await _getPricesQuery.GetStoingPrices(_product);

        if (prices != null && prices.Count > 0)
        {
            if (prices[0].Availability == false) return text += $": <i>нет в наличии</i>";

            text += "\n\n";

            foreach (var price in prices)
            {
                          
                if (price.ProductPrice != 0)
                {
                    text += $"Цена ({price.Amount}): <b>{price.ProductPrice} р./кг</b>\n";
                }
            }
            text += "\n";

            if (prices[0].AvailbleInMoskow != null) text += 
                    $"<i><b>Москва</b>: {prices[0].AvailbleInMoskow}</i>\n";

            if (prices[0].AvailbleInSpb != null) text +=
                    $"<i><b>Санкт-Петербург</b>: {prices[0].AvailbleInSpb}</i>";

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
                url: $"https://100ing.ru/{_postfix}")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "✖️ скрыть сообщение", callbackData: $"!")
            },
        });
    }
}
