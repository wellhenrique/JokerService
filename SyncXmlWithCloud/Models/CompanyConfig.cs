using System.Text.Json.Serialization;

public class CompanyConfig
{
    [JsonPropertyName("_id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("token")]
    public required string Token { get; set; }

    [JsonPropertyName("documentSetup")]
    public required DocumentSetup DocumentSetup { get; set; }

    [JsonPropertyName("cnpj")]
    public required string Cnpj { get; set; }

    public string GetFolderPath()
    {
        return $"{DocumentSetup.FoldersSetup.FolderPath}";
    }
}
