using PriceParser.Interfaces;

namespace PriceParser.Settings;

public class KupiFlakonParserSettings : IParserSettings
{
    private string _postfix;

    public KupiFlakonParserSettings(string postfix)
    {
        _postfix = postfix;
    }

    public string BaseUrl => "https://kupi-flakon.ru/";

    public string Postfix => _postfix;

    public string GetFullUrl()
    {
        return $"{BaseUrl}/{Postfix}";
    }
}
