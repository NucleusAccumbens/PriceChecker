using Bot.Common.Abstractions;

namespace Bot.Messages.GeneralMessages;

public class WordWrongMessage : BaseMessage
{
    private readonly int _trysCount;

    public WordWrongMessage(int trysCount)
    {
        _trysCount = trysCount;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => null;

    private string GetMessage()
    {
        string text = "Введённое слово неверно! Введи слово ещё раз или покинь чат\n\n";

        if (_trysCount > 1) { text += $"<i>Осталось {_trysCount} попытки</i>"; }
        else text += $"<i>Осталась {_trysCount} попытка</i>";

        return text;
    }
}
