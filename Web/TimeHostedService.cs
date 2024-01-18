using Application.Common.Interfaces;
using Application.Prices.Commands;
using Application.Prices.Queries;
using Application.TlgUsers.Queries;
using Bot.Common.Interfaces;
using Infrastructure.Persistence;
using PriceParser;

namespace Web;

public class TimeHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;

    private readonly ILogger<TimeHostedService> _logger;

    private readonly IMemoryCachService _memoryCachService;

    private Timer? _timer = null;

    public TimeHostedService(ILogger<TimeHostedService> logger, IMemoryCachService memoryCachService)
    {
        _logger = logger;
        _memoryCachService = memoryCachService;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(CheckPrices, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(10));

        return Task.CompletedTask;
    }

    private void CheckPrices(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);

        DoWork();
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async Task DoWork()
    {
        using (IBotDbContext context = new ThisBotDbContext())
        {
            var query = new GetPricesQuery(context);

            var userQuery = new GetTlgUsersQuery(context);

            var command = new CreatePriceCommand(context);

            var checker = new PriceChecker(command, query, userQuery, _memoryCachService);

            await checker.CheckStoingProductPrices();

            await checker.CheckFlaconProductPrices();
        }
    }
}
