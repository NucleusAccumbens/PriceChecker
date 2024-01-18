using PriceParser.Interfaces;
using System.Net;

namespace PriceParser.Services;

public class HtmlLoader
{
    private readonly HttpClient _client;

    private readonly string _url;

    public HtmlLoader(IParserSettings settings)
    {
        _url = settings.GetFullUrl();

        _client = new HttpClient();
    }

    public async Task<string?> GetSourceByUrlAsync()
    {
        var response = await _client.GetAsync(_url);

        if (response != null && response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }
}
