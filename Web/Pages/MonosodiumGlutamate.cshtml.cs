using Application.Prices.Interfaces;
using Bot.Common.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;
using Web.Models;

namespace Web.Pages;

public class MonosodiumGlutamateModel : PageModel
{
    private readonly IGetPricesQuery _getPricesQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly ChartsHelper _chartsHelper;

    public MonosodiumGlutamateModel(IGetPricesQuery getPricesQuery, IMemoryCachService memoryCachService)
    {
        _getPricesQuery = getPricesQuery;
        _memoryCachService = memoryCachService;
        _chartsHelper = new(_getPricesQuery, _memoryCachService);
    }

    public ChartJs? Chart { get; set; }

    public string ChartJson { get; set; }

    public void OnGet()
    {
        Chart = _chartsHelper.GetChart(Product.MonosodiumGlutamate);

        ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        });
    }
}
