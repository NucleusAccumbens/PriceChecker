using Application.TlgUsers.Interfaces;
using Bot.Messages.GeneralMessages;
using Domain.Entities;

namespace Bot.Commands.GeneralCommands.TextCommands;

public class PasswordTextCommand : BaseTextCommand
{
    private readonly string _failMessage = 
        "Введённое слово неверно.\n\n" +
        "<b>Ты исчерпал все попытки.</b> Чтобы пользоваться ботом, " +
        "уточни кодовое слово и попробуй авторизироваться снова через сутки.";
    
    private readonly MenuMessage _menuMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICreateTlgUserCommand _createTlgUserCommand;

    private WordWrongMessage _wordWrongMessage;

    public PasswordTextCommand(IMemoryCachService memoryCachService, ICreateTlgUserCommand createTlgUserCommand)
    {
        _memoryCachService = memoryCachService;
        _createTlgUserCommand = createTlgUserCommand;
    }

    public override string Name => "password";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            if (update.Message.Text == "Drops")
            {
                _memoryCachService.SetMemoryCach(chatId, String.Empty);

                _memoryCachService.SetMemoryCach(chatId, 0);

                TlgUser user = new()
                {
                    ChatId = chatId,
                    Username = update.Message.Chat.Username,
                    IsAdmin = false,
                    IsKicked = false
                };

                await _createTlgUserCommand.CreateTlgUserAsync(user);
                
                await _menuMessage.SendMessage(chatId, client);
            }
            else
            {
                int trysCount = _memoryCachService.GetTrysCountFromMemoryCach(chatId);

                if (trysCount > 1)
                {
                    _memoryCachService.SetMemoryCach(chatId, trysCount - 1);
                    
                    _wordWrongMessage = new(trysCount - 1);

                    await _wordWrongMessage.SendMessage(chatId, client);
                }
                if (trysCount == 1)
                {
                    _memoryCachService.SetMemoryCach(chatId, trysCount--);
                    
                    await MessageService.SendMessage(chatId, client, _failMessage, null);

                    return;
                }
                if (trysCount == 0)
                {
                    await MessageService.SendMessage(chatId, client,
                        "Прошло менее суток с предыдущей попытки авторизации.", null);

                    return;
                }
            }
        }
    }
}
