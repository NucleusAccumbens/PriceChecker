namespace Application.TlgUsers.Interfaces;

public interface IGetTlgUsersQuery
{
    Task<List<TlgUser>> GetAllTlgUsersAsync();

    Task<TlgUser?> GetTlgUserAsync(long chatId);
}
