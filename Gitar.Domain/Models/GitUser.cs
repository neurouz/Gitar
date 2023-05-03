using Gitar.Domain.Common;

namespace Gitar.Domain.Models;

public class GitUser : SoftDeleteAuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public long? GithubId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public int PublicRepoCount { get; set; }
    public int Following { get; set; }
    public int Followers { get; set; }
    public string? ProfileUrl { get; set; }
    public string? Blog { get; set; }
}
