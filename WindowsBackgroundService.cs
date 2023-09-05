using System.Reflection;

namespace App.WindowsService;

public sealed class WindowsBackgroundService : BackgroundService
{
    private readonly Main _mainService;
    private readonly ILogger<WindowsBackgroundService> _logger;

    public WindowsBackgroundService(
        Main mainService,
        ILogger<WindowsBackgroundService> logger) =>
        (_mainService, _logger) = (mainService, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // string message = $"UserProfile: {Environment.SpecialFolder.UserProfile}";
                // _logger.LogWarning(message);
                // string message1 = $"Sysotem.Envirnment.UserName: {System.Environment.UserName}";
                // _logger.LogWarning(message1);

                // string message2 = $"Assembly.GetEntryAssembly: {Assembly.GetEntryAssembly().Location}";
                // _logger.LogWarning(message2);

                string message = $"Environment.SpecialFolder.ApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}";
                _logger.LogWarning(message);
                string message1 = $"Environment.SpecialFolder.UserProfile: {Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}";
                _logger.LogWarning(message1);

                Main app = new();
                app.Run();
            }
        }
        catch (TaskCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }
}