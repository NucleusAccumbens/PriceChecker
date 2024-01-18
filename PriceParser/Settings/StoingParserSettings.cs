using PriceParser.Interfaces;

namespace PriceParser.Settings;

public class StoingParserSettings : IParserSettings
{
    private string _postfix;

    public StoingParserSettings(string postfix) 
    {
        _postfix = postfix;
    }

    public string BaseUrl => "https://100ing.ru";

    public string Postfix => _postfix;

    public string GetFullUrl()
    {
        return $"{BaseUrl}/{Postfix}";
    }
}
