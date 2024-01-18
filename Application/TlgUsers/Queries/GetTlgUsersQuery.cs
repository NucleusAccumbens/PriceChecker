namespace Application.TlgUsers.Queries;

public class GetTlgUsersQuery : IGetTlgUsersQuery
{
    private readonly IBotDbContext _context;

    public GetTlgUsersQuery(IBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<TlgUser>> GetAllTlgUsersAsync()
    {
        return await _context.TlgUsers
            .ToListAsync();
    }

    public async Task<TlgUser?> GetTlgUserAsync(long chatId)
    {
        return await _context.TlgUsers
            .Where(u => u.ChatId == chatId)
            .SingleOrDefaultAsync();
    }
}
