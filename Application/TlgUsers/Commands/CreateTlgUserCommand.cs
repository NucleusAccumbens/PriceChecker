namespace Application.TlgUsers.Commands;

public class CreateTlgUserCommand : ICreateTlgUserCommand
{
    private readonly IBotDbContext _context;

    public CreateTlgUserCommand(IBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateTlgUserAsync(TlgUser tlgUser)
    {
        await _context.TlgUsers
            .AddAsync(tlgUser);
        
        await _context.SaveChangesAsync();
    }
}
