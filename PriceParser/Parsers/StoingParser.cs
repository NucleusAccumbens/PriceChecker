using AngleSharp.Html.Dom;
using PriceParser.Interfaces;

namespace PriceParser.Parsers;

public class StoingParser : IParser<string[]>
{
    private readonly List<string> _prices = new();

    private readonly List<string> _amounts = new();

    private readonly List<string> _availability = new();

    private readonly List<string> _availableCount = new();

    private readonly List<string> _availbleCities = new();

    public string[] ParsePrices(IHtmlDocument document)
    {
        var priceItems = document.QuerySelectorAll("span")
            .Where(item => item.ClassName != null && item.ClassName.Contains("one"));

        foreach (var priceItem in priceItems)
        {
            _prices.Add(priceItem.TextContent);
        }

        return _prices.ToArray();
    }

    public string[] ParseAmount(IHtmlDocument document)
    {
        var amountItems = document.QuerySelectorAll("span")
            .Where(item => item.ClassName != null && item.ClassName.Contains("two"));

        foreach (var amountItem in amountItems)
        {
            int index = amountItem.TextContent.IndexOf('д') - 1;
            
            _amounts.Add(
                amountItem.TextContent
                .Substring(0, index));
        }

        return _amounts.ToArray();
    }

    public string[] ParseAvailability(IHtmlDocument document)
    {
        var availItems = document.QuerySelectorAll("span")
            .Where(item => item.ClassName != null && item.ClassName.Contains("notAvailable"));

        foreach (var availItem in availItems)
        {
            _availability.Add(
                availItem.TextContent);
        }

        return _availability.ToArray();
    }

    public string[] ParseAvailableCount(IHtmlDocument document)
    {
        var countItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("place"))
            .Where(item => item.LastChild != null);

        foreach (var countItem in countItems)
        {
            var nlg = countItem.TextContent;
            
            _availableCount.Add(countItem.LastChild.TextContent);
        }

        return _availableCount.ToArray();
    }

    public string[] ParseAvailableCities(IHtmlDocument document)
    {
        var cityItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("place"))
            .Where(item => item.Children.ElementAt(1).TextContent == "Москва" ||
            item.Children.ElementAt(1).TextContent == "Санкт-Петербург");

        foreach (var city in cityItems)
        {
            _availbleCities.Add(city.Children.ElementAt(1).TextContent);
        }

        return _availbleCities.ToArray();
    }
}