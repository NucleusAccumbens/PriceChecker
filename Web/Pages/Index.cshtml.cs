using Application.Common.Interfaces;
using Bot.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        string[] _defoultDates = { "26.11.2022", "27.11.2022", "28.11.2022", "29.11.2022", "30.11.2022", "01.12.2022", "02.12.2022" };

        decimal[] _pricesFrom25kg = { 234.68m, 234.68m, 234.68m, 232.5m, 234.68m, 234.68m, 234.68m };

        decimal[] _pricesFrom325kg = { 229.95m, 231.00m, 231.5m, 232.00m, 233.55m, 227.95m, 229.95m };


        private readonly IBotDbContext _context;

        private readonly IMemoryCachService _memoryCachService;

        public IndexModel(IBotDbContext context, IMemoryCachService memoryCachService)
        {
            _context = context;
            _memoryCachService = memoryCachService;
        }

        public ChartJs Chart { get; set; }

        public string ChartJson { get; set; }

        public void OnGet()
        {
            Chart = new ChartJs()
            {
                type = "line",
                responsive = true,
                data = new Data()
                {
                    labels = _defoultDates,
                    datasets = new Dataset[]
                    {
                        new Dataset()
                        {
                            label = "Лимонная кислота от 25 кг до 300 кг",
                            data = _pricesFrom25kg,
                            backgroundColor = new string[] { "rgba(54, 162, 235, 0.2)" },
                            borderColor = new string[] { "rgba(54, 162, 235, 1)" },
                            borderWidth = 1,
                            stepped = true
                        },
                        new Dataset()
                        {
                            label = "Лимонная кислота от 325 кг до 20 000 кг",
                            data = _pricesFrom325kg,
                            backgroundColor = new string[] { "rgba(255, 99, 132, 0.2)" },
                            borderColor = new string[] { "rgba(255, 99, 132, 1)" },
                            borderWidth = 1,
                            stepped = true
                        }
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
                            fill = true
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
            int period = _memoryCachService.GetPeriodFromMemorycach();

            List<string> dates = new();

            if (period == 1)
            {
                return dates.ToArray();
            }

            if (period == 2)
            {
                return dates.ToArray();
            }

            if (period == 3)
            {
                return dates.ToArray();
            }

            if (period == 4)
            {
                return dates.ToArray();
            }

            if (period == 5)
            {
                return dates.ToArray();
            }

            return _defoultDates;
        }

        private decimal[] GetPrices()
        {
            List<decimal> prices = new List<decimal>();

            return prices.ToArray();
        }
    }
}