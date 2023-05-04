using System.Text.Json.Serialization;

namespace Gitar.Domain.Models;

public class GitRepository
{
    [JsonPropertyName("id")]
    public long RepositoryId { get; set; }
    public string? Name { get; set; }
    [JsonPropertyName("html_url")]
    public string? Url { get; set; }
    public string? Description { get; set; }
    public bool Fork { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreateDate { get; set; }
    [JsonPropertyName("pushed_at")]
    public DateTime PushDate { get; set; }
    public string? Language { get; set; }
    [JsonPropertyName("forks")]
    public int ForkCount { get; set; }
}
