using System.Text.Json.Serialization;

public class RegexOptions
{
    [JsonPropertyName("ignore")]
    public bool? Ignore { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}