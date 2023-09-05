using App.WindowsService;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

using CliWrap;

const string ServiceName = ".NET Joke Service";

if (args is { Length: 1 })
{
    try
    {
        string executablePath =
            Path.Combine(AppContext.BaseDirectory, "App.WindowsService.exe");

        if (args[0] is "/Install")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
                .ExecuteAsync();
        }
        else if (args[0] is "/Uninstall")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "stop", ServiceName })
                .ExecuteAsync();

            await Cli.Wrap("sc")
                .WithArguments(new[] { "delete", ServiceName })
                .ExecuteAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }

    return;
}

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = ".NET Joke Service";
    })
    .ConfigureServices((context, services) =>
    {
        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(services);

        services.AddSingleton<Main>();
        services.AddHostedService<WindowsBackgroundService>();

        // See: https://github.com/dotnet/runtime/issues/47303
        services.AddLogging(builder =>
            {
                builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                builder.AddConsole(); // You can add other logging providers as needed
                builder.AddEventLog(); // Add EventLog as a logging provider if required
                builder.AddDebug();
                builder.AddEventSourceLogger();
            });

    });

IHost host = builder.Build();
host.Run();