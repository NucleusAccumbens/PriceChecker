using Application.Common.EnumParsers;
using Application.Prices.Interfaces;
using Bot.Common.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Pages;

public class KupiFlakonModel : PageModel
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetPricesQuery _getPricesQuery;

    public KupiFlakonModel(IMemoryCachService memoryCachService, IGetPricesQuery getPricesQuery)
    {
        _memoryCachService = memoryCachService;
        _getPricesQuery = getPricesQuery;
    }

    public ChartJs? Chart { get; set; }

    public string ChartJson { get; set; }

    public void OnGet()
    {
        int daysCount = _memoryCachService.GetPeriodFromMemorycach();
        
        string[] dates = GetDates();

        decimal[] browmBottle50mlChinaPrices = GetBrowmBottle50mlChinaPrices(daysCount);

        decimal[] browmBottle50mlCzechPrices = GetBrowmBottle50mlCzechPrices(daysCount);

        decimal[] blackBottle50mlPrices = GetBlackBottle50mlPrices(daysCount);

        decimal[] roundPipettePrices = GetRoundPipettePrices(daysCount);

        decimal[] squarePipettePrices = GetSquarePipettePrices(daysCount);

        Chart = new ChartJs()
        {
            type = "line",
            responsive = true,
            data = new Data()
            {
                labels = dates,
                datasets = new Dataset[]
                {
                    new Dataset()
                    {
                        label = $"{ProductEnumParser.GetProductStringValue(Product.BrowmBottle50mlChina)}",
                        data = browmBottle50mlChinaPrices,
                        borderColor = new string[] { "rgba(255, 5, 5, 1)" },
                        borderWidth = 1,
                        stepped = true
                    },
                    new Dataset()
                    {
                        label = $"{ProductEnumParser.GetProductStringValue(Product.BrowmBottle50mlCzech)}",
                        data = browmBottle50mlCzechPrices,
                        borderColor = new string[] { "rgba(2, 6, 240, 1)" },
                        borderWidth = 1,
                        stepped = true
                    },
                    new Dataset()
                    {
                        label = $"{ProductEnumParser.GetProductStringValue(Product.BlackBottle50ml)}",
                        data = blackBottle50mlPrices,
                        borderColor = new string[] { "rgba(0, 0, 0, 1)" },
                        borderWidth = 1,
                        stepped = true
                    },
                    new Dataset()
                    {
                        label = $"{ProductEnumParser.GetProductStringValue(Product.RoundPipette)}",
                        data = roundPipettePrices,
                        borderColor = new string[] { "rgba(34, 240, 2, 1)" },
                        borderWidth = 1,
                        stepped = true
                    },
                    new Dataset()
                    {
                        label = $"{ProductEnumParser.GetProductStringValue(Product.SquarePipette)}",
                        data = squarePipettePrices,
                        borderColor = new string[] { "rgba(12, 208, 247, 1)" },
                        borderWidth = 1,
                        stepped = true
                    },
                }
            },
            options = new Options()
            {
                scales = new Scales()
                {
                    yAxes = new yAxes[]
                    {
                        new yAxes()
                        {
                            ticks = new Ticks()
                            {
                                beginAtZero = false,
                            },
                            display = true,
                        },
                    },
                },
                elements = new Elements()
                {
                    line = new Line()
                    {
                        stepped = true,
                        fill = false,
                    }
                }
            }
        };

        ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        });
    }

    private string[] GetDates()
    {
        List<string> dates = new List<string>();

        int daysCount = _memoryCachService.GetPeriodFromMemorycach();

        if (daysCount > 0)
        {
            DateTime startDate = DateTime.UtcNow.Date - TimeSpan.FromDays(daysCount - 1);

            while (startDate != DateTime.UtcNow.Date.AddDays(1))
            {
                dates.Add(startDate.ToShortDateString());

                startDate += TimeSpan.FromDays(1);
            }

            return dates.ToArray();
        }

        else return _getPricesQuery.GetAllKupiFlakonPriceDates(Product.BrowmBottle50mlChina).Result;
    }

    private decimal[] GetBrowmBottle50mlChinaPrices(int daysCount)
    {
        List<decimal> prc = new();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
                        .GetMaxKupiFlakonPricesForPeriod(Product.BrowmBottle50mlChina, daysCount).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
        else
        {
            var prices = _getPricesQuery
                .GetMaxKupiFlakonPrices(Product.BrowmBottle50mlChina).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
    }

    private decimal[] GetBrowmBottle50mlCzechPrices(int daysCount)
    {
        List<decimal> prc = new();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
            .GetMaxKupiFlakonPricesForPeriod(Product.BrowmBottle50mlCzech, daysCount).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
        else
        {
            var prices = _getPricesQuery
                .GetMaxKupiFlakonPrices(Product.BrowmBottle50mlCzech).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
    }

    private decimal[] GetBlackBottle50mlPrices(int daysCount)
    {
        List<decimal> prc = new();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
                        .GetMaxKupiFlakonPricesForPeriod(Product.BlackBottle50ml, daysCount).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
        else
        {
            var prices = _getPricesQuery
                .GetMaxKupiFlakonPrices(Product.BlackBottle50ml).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
    }

    private decimal[] GetRoundPipettePrices(int daysCount)
    {
        List<decimal> prc = new();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
                        .GetMaxKupiFlakonPricesForPeriod(Product.RoundPipette, daysCount).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
        else
        {
            var prices = _getPricesQuery
                .GetMaxKupiFlakonPrices(Product.RoundPipette).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
    }

    private decimal[] GetSquarePipettePrices(int daysCount)
    {
        List<decimal> prc = new();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
           .GetMaxKupiFlakonPricesForPeriod(Product.SquarePipette, daysCount).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
        else
        {
            var prices = _getPricesQuery
                .GetMaxKupiFlakonPrices(Product.SquarePipette).Result;

            if (prices != null)
            {
                foreach (var price in prices)
                {
                    prc.Add(price.ProductPrice);
                }
            }

            return prc.ToArray();
        }
    }
}
