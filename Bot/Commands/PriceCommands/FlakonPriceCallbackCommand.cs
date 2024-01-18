using Application.Prices.Interfaces;
using Bot.Messages.PriceMessages;
using Domain.Enums;

namespace Bot.Commands.PriceCommands;

internal class FlakonPriceCallbackCommand : BaseCallbackCommand
{
    private readonly IGetPricesQuery _getPricesQuery;

    private KupiFlakonPriceMessage _message; 

    public FlakonPriceCallbackCommand(IGetPricesQuery getPricesQuery)
    {
        _getPricesQuery = getPricesQuery;
    }
    
    public override char CallbackDataCode => 'd';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data == "dBottleBrown1")
            {
                _message = new(_getPricesQuery, Product.BrowmBottle50mlChina,
                    "50ml_brown_QY?search=YTA-1050");

                await _message.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "dBottleBrown2")
            {
                _message = new(_getPricesQuery, Product.BrowmBottle50mlCzech,
                    "50ml_brown_St");

                await _message.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "dBottleBlack")
            {
                _message = new(_getPricesQuery, Product.BlackBottle50ml,
                    "50ml_black");

                await _message.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "dPipetteRound")
            {
                _message = new(_getPricesQuery, Product.RoundPipette,
                    "50_pip_gold_bl_CH_sloped?search=12%2024");

                await _message.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == "dPipetteSq")
            {
                _message = new(_getPricesQuery, Product.SquarePipette,
                    "pip_gold_black_HPT_sloped_50?search=12%2024");

                await _message.SendMessage(chatId, client);

                return;
            }
        }
    }
}
