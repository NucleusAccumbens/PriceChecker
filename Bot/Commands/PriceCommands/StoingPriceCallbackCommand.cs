using Application.Prices.Interfaces;
using Bot.Messages.PriceMessages;
using Domain.Enums;

namespace Bot.Commands.PriceCommands;

public class StoingPriceCallbackCommand : BaseCallbackCommand
{
    private readonly IGetPricesQuery _getPricesQuery;

    private StoingPriceMessage _stoingPriceMessage;

    public StoingPriceCallbackCommand(IGetPricesQuery getPricesQuery)
    {
        _getPricesQuery = getPricesQuery;
    }

    public override char CallbackDataCode => 'c';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data == "cLemonAcid")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.LemonAcid,
                    "product/limonnaya-kislota_old/400/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cMalicAcid")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.MalicAcid,
                    "product/yablochnaya-malonovaya-kislota-e-296/647/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cSuccinicAcid")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.SuccinicAcid,
                    "product/iantarnaia-kislota-e363-italiia-25-kg/7720/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cTartaricAcid")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.TartaricAcid,
                    "product/vinnaya-kislota-e334/184/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cSalt")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.Salt,
                    "product/sol-ekstra-nds-10/10368/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cPotassiumChloride")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.PotassiumChloride,
                    "product/kaliia-khlorid/?ysclid=lbamv1c2bm583765548");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cPropyleneGlycol")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.PropyleneGlycol,
                    "product/propilenglikol-uspep-1kg/11170/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cMonosodiumGlutamate")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.MonosodiumGlutamate,
                    "product/glutamat-natriya-razmer-chastic_-60-120-mesh/198/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cSodiumBenzoate")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.SodiumBenzoate,
                    "product/benzoat-natriia-foodchem-granuly-25kg/12233/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cPotassiumSorbate")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.PotassiumSorbate,
                    "product/sorbat-kaliya-kitay/546/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "cPolysorbate80")
            {
                _stoingPriceMessage = new(_getPricesQuery, Product.Polysorbate80,
                    "product/polisorbat-tvin-80-radiamuls/11979/");

                await _stoingPriceMessage.SendMessage(chatId, client);

                return;
            }
        }
    }
}
