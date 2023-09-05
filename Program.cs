using App.WindowsService;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

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