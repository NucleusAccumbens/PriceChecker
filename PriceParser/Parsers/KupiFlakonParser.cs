using AngleSharp.Html.Dom;

namespace PriceParser.Parsers;

public class KupiFlakonParser
{
    private readonly List<string> _prices = new();

    private readonly List<string> _amounts = new();

    public string[] ParsePrices(IHtmlDocument document)
    {
        var priceItems = document.QuerySelectorAll("span")
            .Where(item => item.ClassName != null && item.ClassName.Contains("price-new"));

        foreach (var item in priceItems)
        {
            _prices.Add(item.TextContent);
        }

        return _prices.ToArray();
    }

    public string[] ParseAmounts(IHtmlDocument document)
    {
        var amountItems = document.QuerySelectorAll("span")
            .Where(item => item.ClassName != null && item.ClassName.Contains("price-min"));

        foreach (var amountItem in amountItems)
        {
            _amounts.Add(
                amountItem.TextContent
                .Substring(11, amountItem.TextContent.Length - 12));
        }

        var amountItemsDiscount = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("discount"));

        foreach (var amountItem in amountItemsDiscount)
        {
            int firstIndex = amountItem.TextContent.IndexOf('о');

            int secondIndex = amountItem.TextContent.IndexOf('.');
            
            _amounts.Add(
                amountItem.TextContent
                .Substring(firstIndex, amountItem.TextContent.Length - firstIndex)
                .Remove(secondIndex));
        }

        _amounts.Add("от 1 шт.");

        return _amounts.ToArray();
    }

    public bool ParseAvailability(IHtmlDocument document)
    {
        var availItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("prod-stock"));

        foreach (var availItem in availItems)
        {
            if (availItem.TextContent.Contains("Есть в наличии"))
            {
                return true;
            }
        }

        return false;
    }
}
