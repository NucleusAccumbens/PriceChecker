using Bot.Commands.ChartCommands;
using Bot.Commands.GeneralCommands.CallbackCommand;
using Bot.Commands.GeneralCommands.CallbackCommands;
using Bot.Commands.GeneralCommands.TextCommand;
using Bot.Commands.GeneralCommands.TextCommands;
using Bot.Commands.PriceCommands;
using Bot.Common;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection AddTelegramBotServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<TelegramBot>();
        services.AddSingleton<IMemoryCachService, MemoryCachService>();
        services.AddScoped<ICommandAnalyzer, CommandAnalyzer>();
        services.AddScoped<BaseTextCommand, StartTextCommand>();
        services.AddScoped<BaseTextCommand, PasswordTextCommand>();
        services.AddScoped<BaseCallbackCommand, PriceMenuCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, StoingPriceCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, MenuCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ChartMenuCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ChoiceWebsiteCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, FlakonPriceCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, HideMessageCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ChoicePeriodCallbackCommand>();
        
        return services;
    }
}
