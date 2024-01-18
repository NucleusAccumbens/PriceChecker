using Application.Common.EnumParsers;
using Application.Prices.Interfaces;
using Application.TlgUsers.Interfaces;
using Bot.Common.Interfaces;
using Bot.Common.Services;
using Domain.Entities;
using Domain.Enums;
using PriceParser.Interfaces;
using PriceParser.Services;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceParser;

public class PriceChecker : IPriceChecker
{
    private readonly string[] _stoingPostfixis = {
        "product/limonnaya-kislota_old/400/",
        "product/yablochnaya-malonovaya-kislota-e-296/647/",
        "product/iantarnaia-kislota-e363-italiia-25-kg/7720/",
        "product/vinnaya-kislota-e334/184/",
        "product/sol-ekstra-nds-10/10368/",
        "product/kaliia-khlorid/?ysclid=lbamv1c2bm583765548",
        "product/propilenglikol-uspep-1kg/11170/",
        "product/glutamat-natriya-razmer-chastic_-60-120-mesh/198/",
        "product/benzoat-natriia-foodchem-granuly-25kg/12233/",
        "product/sorbat-kaliya-kitay/546/",
        "product/polisorbat-tvin-80-radiamuls/11979/"
    };

    private readonly Product[] _stoingProducts =
    {
        Product.LemonAcid,
        Product.MalicAcid,
        Product.SuccinicAcid,
        Product.TartaricAcid,
        Product.Salt,
        Product.PotassiumChloride,
        Product.PropyleneGlycol,
        Product.MonosodiumGlutamate,
        Product.SodiumBenzoate,
        Product.PotassiumSorbate,
        Product.Polysorbate80,
    };

    private readonly string[] _flakonPostfix =
    {
        "50ml_brown_QY?search=YTA-1050",
        "50ml_brown_St",
        "50ml_black",
        "50_pip_gold_bl_CH_sloped?search=12%2024",
        "pip_gold_black_HPT_sloped_50?search=12%2024"
    };

    private readonly Product[] _flakonProducts =
    {
        Product.BrowmBottle50mlChina,
        Product.BrowmBottle50mlCzech,
        Product.BlackBottle50ml,
        Product.RoundPipette,
        Product.SquarePipette
    };

    private readonly ICreatePriceCommand _createPriceCommand;

    private readonly IGetPricesQuery _getPricesQuery;

    private readonly IGetTlgUsersQuery _getTlgUsersQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly ITelegramBotClient _client = new TelegramBotClient("5809176030:AAFoJH55O588AibBwG-0V8RRL8T4yu3bonI");

    public PriceChecker(ICreatePriceCommand createPriceCommand, IGetPricesQuery getPricesQuery, 
        IGetTlgUsersQuery getTlgUsersQuery, IMemoryCachService memoryCachService)
    {        
        _createPriceCommand = createPriceCommand;
        _getPricesQuery = getPricesQuery;
        _getTlgUsersQuery = getTlgUsersQuery;
        _memoryCachService = memoryCachService;
    }

    public async Task CheckStoingProductPrices()
    {
        for (int i = 0; i < _stoingPostfixis.Length; i++)
        {
            var service = new StoingPriceService(_stoingPostfixis[i]);

            var prices = service.GetPrices();

            var amounts = service.GetAmounts();

            bool availability = service.GetAvailability();

            var inMoskow = service.GetAvailbleCountInMoskow();

            var inSpb = service.GetAvailbleCountInSpb();

            if (availability == true && amounts != null && amounts.Length > 0)
            {
                for (int j = 0; j < prices.Length; j++)
                {
                    await StoingNotifi(prices[j], _stoingProducts[i], amounts[j], _stoingPostfixis[i]);

                    StoingPrice price = new()
                    {
                        Product = _stoingProducts[i],
                        ProductPrice = prices[j],
                        Amount = amounts[j],
                        Availability = availability,
                        AvailbleInMoskow = inMoskow,
                        AvailbleInSpb = inSpb,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _createPriceCommand.CreateStoingPriceAsync(price);
                }
            }

            else if (availability == false)
            {
                StoingPrice price = new()
                {
                    Product = _stoingProducts[i],
                    ProductPrice = 0,
                    Amount = null,
                    Availability = availability,
                    AvailbleInMoskow = null,
                    AvailbleInSpb = null,
                    CreatedAt = DateTime.UtcNow
                };

                await _createPriceCommand.CreateStoingPriceAsync(price);
            }
        }
    }

    public async Task CheckFlaconProductPrices()
    {
        for (int i = 0; i < _flakonPostfix.Length; i++) 
        { 
            var service = new KupiFlakonParserService(_flakonPostfix[i]);

            var prices = service.GetPrices();

            var amounts = service.GetAmounts();

            bool? availability = service.GetAvailability();

            if (availability == true && amounts != null && amounts.Length > 0)
            {       
                for (int j = 0; j < prices.Length; j++)
                {
                    await FlakonNotifi(prices[j], _flakonProducts[i], amounts[j], _flakonPostfix[i]);

                    KupiFlakonPrice price = new()
                    {
                        Product = _flakonProducts[i],
                        ProductPrice = prices[j],
                        Amount = amounts[j],
                        Availability = true,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _createPriceCommand.CreateFlakonPriceAsync(price);
                }                   
            }

            else if (availability == false)
            {
                KupiFlakonPrice price = new()
                {
                    Product = _flakonProducts[i],
                    ProductPrice = 0,
                    Amount = null,
                    Availability = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _createPriceCommand.CreateFlakonPriceAsync(price);
            }
        }
    }

    private async Task StoingNotifi(decimal currentPrice, Product product, string amount, string postfix)
    {
        _memoryCachService.SetMemoryCach(0);

        var tlgUsers = await _getTlgUsersQuery.GetAllTlgUsersAsync();

        var lastPrice = await _getPricesQuery.GetStoingPrice(product, amount);

        var lastPrices = await _getPricesQuery.GetStoingPrices(product);

        InlineKeyboardMarkup button = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl(text: "Перейти на страницу товара",
                url: $"https://100ing.ru/{postfix}")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl(text: "📈 Посмотреть график",
                url: $"https://a15687-8a2e.g.d-f.pw/{product}")
            },
        });

        if (lastPrices != null && lastPrices.Count == 1 && lastPrices[0].ProductPrice == 0 && currentPrice > 0)
        {
            string message =
                $"Товар " +
                $"<b>\"{ProductEnumParser.GetProductStringValue(product)}\"</b> " +
                $"({amount}) " +
                $"появился в наличии.\n\n" +
                $"Цена: <b>{currentPrice} р./кг</b>";

            foreach (var user in tlgUsers)
            {
                await MessageService
                    .SendMessage(user.ChatId, _client, message, button);
            }
        }

        if (lastPrice != null)
        {
            if (currentPrice == 0)
            {
                string message =
                $"Товар " +
                $"<b>\"{ProductEnumParser.GetProductStringValue(product)}\"</b> " +
                $"закончился.";

                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }
            
            if (lastPrice.ProductPrice > currentPrice)
            {
                decimal proc = ((lastPrice.ProductPrice - currentPrice)/lastPrice.ProductPrice) * 100;

                string message = 
                    $"Цена на товар " +
                    $"<b>\"{ProductEnumParser.GetProductStringValue(lastPrice.Product)}\"</b> " +
                    $"({amount}) " +
                    $"понизилась на {Math.Round(proc, 1)} %\n\n" +
                    $"Новая цена: <b>{currentPrice} р./кг</b>";

                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }

            if (lastPrice.ProductPrice < currentPrice)
            {
                decimal proc = ((currentPrice - lastPrice.ProductPrice) / lastPrice.ProductPrice) * 100;

                string message = 
                    $"Цена на товар " +
                    $"<b>\"{ProductEnumParser.GetProductStringValue(lastPrice.Product)}\"</b> " +
                    $"({amount}) " +
                    $"повысилась на {Math.Round(proc, 1)} %\n\n" +
                    $"Новая цена: <b>{currentPrice} р./кг</b>";

                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }
        }
    }

    private async Task FlakonNotifi(decimal currentPrice, Product product, string amount, string postfix)
    {
        _memoryCachService.SetMemoryCach(0);

        var tlgUsers = await _getTlgUsersQuery.GetAllTlgUsersAsync();
        
        var lastPrice = await _getPricesQuery.GetKupiFlakonPrice(product, amount);

        var lastPrices = await _getPricesQuery.GetKupiFlakonPrices(product);

        InlineKeyboardMarkup button = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl(text: "Перейти на страницу товара",
                url: $"https://kupi-flakon.ru/{postfix}")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl(text: "📈 Посмотреть график",
                url: $"https://a15687-8a2e.g.d-f.pw/kupiFlakon")
            },
        });

        if (lastPrices != null && lastPrices.Count == 1 && lastPrices[0].ProductPrice == 0 && currentPrice > 0)
        {
            int index = amount.IndexOf('.');     
            
            string message =
                $"Товар " +
                $"<b>\"{ProductEnumParser.GetProductStringValue(product)}\"</b> " +
                $"({amount.Remove(index)}) " +
                $"появился в наличии.\n\n" +
                $"Цена: <b>{currentPrice} р./шт.</b>";

            foreach (var user in tlgUsers)
            {
                await MessageService
                    .SendMessage(user.ChatId, _client, message, button);
            }
        }

        if (lastPrice != null)
        {
            if (currentPrice == 0)
            {
                string message =
                $"Товар " +
                $"<b>\"{ProductEnumParser.GetProductStringValue(product)}\"</b> " +
                $"закончился.";

                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }

            if (lastPrice.ProductPrice > currentPrice)
            {
                decimal proc = ((lastPrice.ProductPrice - currentPrice) / lastPrice.ProductPrice) * 100;

                int index = amount.IndexOf('.');

                string message =
                    $"Цена на товар " +
                    $"<b>\"{ProductEnumParser.GetProductStringValue(lastPrice.Product)}\"</b> " +
                    $"({amount.Remove(index)}) " +
                    $"понизилась на {Math.Round(proc, 1)} %\n\n" +
                    $"Новая цена: <b>{currentPrice} р./шт.</b>";


                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }

            if (lastPrice.ProductPrice < currentPrice)
            {
                decimal proc = ((currentPrice - lastPrice.ProductPrice) / lastPrice.ProductPrice) * 100;

                int index = amount.IndexOf('.');

                string message =
                    $"Цена на товар " +
                    $"<b>\"{ProductEnumParser.GetProductStringValue(lastPrice.Product)}\"</b> " +
                    $"({amount.Remove(index)}) " +
                    $"повысилась на {Math.Round(proc, 1)} %\n\n" +
                    $"Новая цена: <b>{currentPrice} р./шт.</b>";

                foreach (var user in tlgUsers)
                {
                    await MessageService
                        .SendMessage(user.ChatId, _client, message, button);
                }
            }
        }
    }
}
