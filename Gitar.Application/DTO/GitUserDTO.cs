using Gitar.Domain.Models;
using System.Text.Json.Serialization;

namespace Gitar.Application.DTO;

public class GitUserDTO
{
    [JsonPropertyName("login")]
    public string? Username { get; set; }
    public long? Id { get; set; }
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }
    [JsonPropertyName("public_repos")]
    public int PublicRepoCount { get; set; }
    public int Following { get; set; }
    public int Followers { get; set; }
    [JsonPropertyName("html_url")]
    public string? ProfileUrl { get; set; }
    public string? Blog { get; set; }

    public GitUser DomainModel => new GitUser()
    {
        Name = Username,
        GithubId = Id,
        ImageUrl = AvatarUrl,
        PublicRepoCount = PublicRepoCount,
        Following = Following,
        Followers = Followers,
        ProfileUrl = ProfileUrl,
        Blog = Blog
    };
}
