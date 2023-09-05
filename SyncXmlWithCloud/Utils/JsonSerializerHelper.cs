using System.Text.Json;
public static class JsonSerializerHelper
{
  public static CompanyConfig Deserialize(string filePath)
  {
    return JsonSerializer.Deserialize<CompanyConfig>(filePath)!;
  }
}