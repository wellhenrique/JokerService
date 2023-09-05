using System.Text.Json.Serialization;

public class DocumentSetup
{
    [JsonPropertyName("initialDate")]
    public required DateTime InitialDate { get; set; }

    [JsonPropertyName("documentType")]
    public required string DocumentType { get; set; }

    [JsonPropertyName("foldersSetup")]
    public required FoldersSetup FoldersSetup { get; set; }

    [JsonPropertyName("product")]
    public required string Product { get; set; }
}
