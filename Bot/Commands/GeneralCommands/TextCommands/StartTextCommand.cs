using Application.TlgUsers.Interfaces;
using Bot.Messages.GeneralMessages;

namespace Bot.Commands.GeneralCommands.TextCommand;

public class StartTextCommand : BaseTextCommand
{
    private readonly MenuMessage _menuMessage = new();

    private readonly NewUserMessage _newUserMessage = new();

    private readonly IGetTlgUsersQuery _getTlgUsersQuery;

    private readonly IMemoryCachService _memoryCachService;

    public StartTextCommand(IGetTlgUsersQuery getTlgUsersQuery, IMemoryCachService memoryCachService)
    {
        _getTlgUsersQuery = getTlgUsersQuery;
        _memoryCachService = memoryCachService;
    }

    public override string Name => "/start";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            var user = await _getTlgUsersQuery.GetTlgUserAsync(chatId);

            if (user != null)
            {
                await _menuMessage.SendMessage(chatId, client);

                return;
            }

            if (user == null) 
            {
                var commandState = _memoryCachService.GetCommandStateFromMemoryCach(chatId);


                if (commandState != null && commandState == "password") 
                {
                    await MessageService.SendMessage(chatId, client, 
                        "Прошло менее суток с предыдущей попытки авторизации.", null);

                    return;
                }
                
                _memoryCachService.SetMemoryCach(chatId, "password");

                _memoryCachService.SetMemoryCach(chatId, 3);
                
                await _newUserMessage.SendMessage(chatId, client);
            }
        }
    }
}
