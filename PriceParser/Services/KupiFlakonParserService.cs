using AngleSharp.Html.Parser;
using PriceParser.Parsers;
using PriceParser.Settings;
using System.Globalization;

namespace PriceParser.Services;

public class KupiFlakonParserService
{
    private readonly KupiFlakonParser _parser = new();

    private readonly HtmlParser _htmlParser = new();

    private readonly string _postfix;

    public KupiFlakonParserService(string postfix)
    {
        _postfix = postfix;
    }

    private string? GetSours()
    {
        var parserSettings = new KupiFlakonParserSettings(_postfix);

        var loader = new HtmlLoader(parserSettings);

        return loader.GetSourceByUrlAsync().Result;
    }

    private string[]? ParsePrices()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _parser.ParsePrices(document);
        }

        return null;
    }

    public decimal[] GetPrices()
    {
        List<decimal> priceList = new();

        string[]? resalts = ParsePrices();

        if (resalts != null)
        {
            foreach (var resalt in resalts)
            {
                int index = resalt.IndexOf(' ');

                string strPrice = resalt
                    .Substring(0, index)
                    .Replace('.', ',');

                priceList.Add(Convert.ToDecimal(strPrice, new NumberFormatInfo() { NumberDecimalSeparator = "," }));
            }
        }

        return priceList.ToArray();
    }

    public string[]? GetAmounts()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _parser.ParseAmounts(document);
        }

        return null;
    }

    public bool? GetAvailability()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _parser.ParseAvailability(document);
        }

        return null;
    }
}
