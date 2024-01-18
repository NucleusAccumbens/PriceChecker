using AngleSharp.Html.Parser;
using PriceParser.Parsers;
using PriceParser.Settings;
using System.Globalization;

namespace PriceParser.Services;

public class StoingPriceService
{
    private readonly StoingParser _parser = new StoingParser();

    private readonly HtmlParser _htmlParser = new();

    private readonly string _postfix;

    public StoingPriceService(string postfix)
    {
        _postfix = postfix;
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

                for (int i = 0; i < strPrice.Length; i++)
                {
                    char x = strPrice[i];

                    if (!Int32.TryParse($"{x}", out _) && x != ',')
                    {
                        strPrice = strPrice.Remove(i, 1);
                    }
                }

                priceList.Add(Convert.ToDecimal(strPrice, new NumberFormatInfo() { NumberDecimalSeparator = "," }));
            }
        }

        return priceList.ToArray();
    }

    public string[]? GetAmounts()
    {
        return ParseAmounts(); 
    }

    public bool GetAvailability()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            string[] avaiability = _parser.ParseAvailability(document);

            if (avaiability.Length > 0) return false;
        }

        return true;
    }

    public string? GetAvailbleCountInMoskow()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            var availbleCount = _parser.ParseAvailableCount(document);

            var availbleCities = _parser.ParseAvailableCities(document);

            if (availbleCount != null && availbleCount.Length > 0 &&
                availbleCities != null && availbleCities.Length > 0)
            {
                if (availbleCount.Length == availbleCities.Length &&
                    availbleCount.Length == 1 &&
                    availbleCities[0].Contains("Москва"))
                {
                    return RemoveSpacesAtTheBegining(availbleCount[0]);
                }

                if (availbleCount.Length == availbleCities.Length &&
                    availbleCount.Length == 2)
                {
                    for (int i = 0; i < availbleCount.Length; i++)
                    {
                        if (availbleCities[i].Contains("Москва"))
                            return RemoveSpacesAtTheBegining(availbleCount[i]);
                    }
                }

                else return null;
            }
        }

        return null;
    }

    public string? GetAvailbleCountInSpb()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            var availbleCount = _parser.ParseAvailableCount(document);

            var availbleCities = _parser.ParseAvailableCities(document);

            if (availbleCount != null && availbleCount.Length > 0 &&
                availbleCities != null && availbleCities.Length > 0)
            {
                if (availbleCount.Length == availbleCities.Length &&
                    availbleCount.Length == 1 &&
                    availbleCities[0].Contains("Санкт-Петербург"))
                {
                    return RemoveSpacesAtTheBegining(availbleCount[0]);
                }

                if (availbleCount.Length == availbleCities.Length &&
                    availbleCount.Length > 1)
                {
                    for (int i = 0; i < availbleCount.Length; i++)
                    {
                        if (availbleCities[i].Contains("Санкт-Петербург"))
                            return RemoveSpacesAtTheBegining(availbleCount[i]);
                    }
                }

                else return null;
            }
        }

        return null;
    }

    private string? GetSours()
    {
        var parserSettings = new StoingParserSettings(_postfix);

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

    private string[]? ParseAmounts()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _parser.ParseAmount(document);
        }

        return null;
    }

    private static string RemoveSpacesAtTheBegining(string str)
    {
        string newString = str;
        
        for (int i = 0; i < str.Length; i++) 
        {
            if (str[i] == ' ') newString = str.Remove(0, i + 1);

            else return newString;
        }

        return str;
    }
}
