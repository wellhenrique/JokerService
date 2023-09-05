using System.Text.Json.Serialization;

public class FoldersSetup
{
    [JsonPropertyName("folderPath")]
    public required string FolderPath { get; set; }

    [JsonPropertyName("ignoredPaths")]
    public required string[] IgnoredPaths { get; set; }

    [JsonPropertyName("regex")]
    public RegexOptions? Regex { get; set; }

    [JsonPropertyName("filterByCNPJ")]
    public bool FilterByCNPJ { get; set; }

    [JsonPropertyName("regexXmlProp")]
    public RegexOptions? RegexXmlProp { get; set; }

    public bool IsPathIgnored(string filePath)
    {
        if (IgnoredPaths.Length == 0) return false;

        foreach (string ignoredPath in IgnoredPaths)
        {
            if (filePath.Contains(ignoredPath))
            {
                return true;
            }
        }
        return false;
    }
}