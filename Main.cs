using JokerService.SyncXmlWithCloud.DotEnv;

namespace App.WindowsService
{
  public sealed class Main
  {
    private readonly ILogger<WindowsBackgroundService> _logger;

    public Main(ILogger<WindowsBackgroundService> logger)
    {
      _logger = logger;
    }

    public void Run()
    {
      DotEnv.Load();
      List<CompanyConfig>? configjsonContentList = ConectaFacilDirectoryHelper.LoadConfiguredCompaniesOnConfigsFolder();

      if (configjsonContentList?.Count > 0)
      {
        foreach (var companyConfig in configjsonContentList)
        {
          string xmlSourceFolderPath = companyConfig.GetFolderPath();

          XmlToCloudSyncer syncXmlWithCloud = new(xmlSourceFolderPath, companyConfig);
          syncXmlWithCloud.InitializeWatcher();
        }
      }
      else
      {
        Console.WriteLine("No company configuration files found.");
      }
    }
  }

  readonly record struct Piada(string Setup, string Piunchline);
}
