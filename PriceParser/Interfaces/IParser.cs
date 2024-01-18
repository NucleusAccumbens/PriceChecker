using AngleSharp.Html.Dom;

namespace PriceParser.Interfaces;

public interface IParser<T> where T : class
{
    T ParsePrices(IHtmlDocument document);
}
