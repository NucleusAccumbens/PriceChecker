using Application.Common.EnumParsers;
using Application.Prices.Interfaces;
using Bot.Common.Interfaces;
using Domain.Enums;
using Web.Models;

namespace Web.Helpers;

public class ChartsHelper
{
    private readonly IGetPricesQuery _getPricesQuery;

    private readonly IMemoryCachService _memoryCachService;

    public ChartsHelper(IGetPricesQuery getPricesQuery, IMemoryCachService memoryCachService)
    {
        _getPricesQuery = getPricesQuery;
        _memoryCachService = memoryCachService;
    }

    public ChartJs GetChart(Product product)
    {
        decimal[] prices = GetPrices(product);

        string[] dates = GetDates(product);

        return new ChartJs()
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
                        label = $"{ProductEnumParser.GetProductStringValue(product)}",
                        data = prices,
                        backgroundColor = new string[] { "rgba(54, 162, 235, 0.2)" },
                        borderColor = new string[] { "rgba(54, 162, 235, 1)" },
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
                    line = GetLine(prices[prices.Length - 1])
                }
            }
        };
    }

    private string[] GetDates(Product product)
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

        else return _getPricesQuery.GetAllStoingPriceDates(product).Result;
    }

    private decimal[] GetPrices(Product product)
    {
        List<decimal> prc = new();

        int daysCount = _memoryCachService.GetPeriodFromMemorycach();

        if (daysCount > 0)
        {
            var prices = _getPricesQuery
                .GetMaxStoingPricesForPeriod(product, daysCount).Result;

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
                .GetMaxStoingPrices(product).Result;

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

    private Line GetLine(decimal lastPrise)
    {
        if (lastPrise != 0)
        {
            return new Line()
            {
                stepped = true,
                fill = true,
            };
        }

        else return new Line()
        {
            stepped = true,
            fill = true,
            borderDash = new int[] { 5, 5 }
        };
    }
}
