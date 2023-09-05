public static class ConectaFacilDirectoryHelper
{
  readonly static List<string> _cnpjConfiguredOnConfigsFolder = new();
  readonly static string _pathConfigFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "conectafacil", "configs");

  public static string[] GetJsonFilesFromConfigFolder()
  {

    return Directory.GetFiles(_pathConfigFiles, "*.json");

  }

  public static List<CompanyConfig>? LoadConfiguredCompaniesOnConfigsFolder()
  {
    string[] companiesConfigFiles = GetJsonFilesFromConfigFolder();
    List<CompanyConfig> configuredCompanies = new();

    foreach (string configCompanyJson in companiesConfigFiles)
    {
      string jsonContentInTextFormat = File.ReadAllText(configCompanyJson);
      CompanyConfig companyConfig = JsonSerializerHelper.Deserialize(jsonContentInTextFormat);

      if (companyConfig != null)
      {
        configuredCompanies.Add(companyConfig);
      }

    }

    return configuredCompanies;
  }

  public static void LoadCNPJConfiguredFromConfigsFolder()
  {
    string[] companiesConfigFiles = GetJsonFilesFromConfigFolder();
    foreach (string configCompanyJson in companiesConfigFiles)
    {
      string jsonContentInTextFormat = File.ReadAllText(configCompanyJson);
      CompanyConfig companyConfig = JsonSerializerHelper.Deserialize(jsonContentInTextFormat);

      if (companyConfig.Cnpj != null)
      {
        _cnpjConfiguredOnConfigsFolder.Add(companyConfig.Cnpj);
      }
    }
  }

  public static string? PathConfigFiles
  {
    get { return _pathConfigFiles; }
  }

  public static List<string> CnpjConfiguredOnConfigsFolder
  {
    get { return _cnpjConfiguredOnConfigsFolder; }
  }
}