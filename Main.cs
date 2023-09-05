namespace App.WindowsService
{
  public sealed class Main
  {

    public void Run()
    {
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
