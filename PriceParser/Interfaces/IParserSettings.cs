namespace PriceParser.Interfaces;

public interface IParserSettings
{
    string BaseUrl { get; }

    string Postfix { get; }

    string GetFullUrl();
}
