using Gitar.Domain.Common;

namespace Gitar.Domain.Models;

public class GitUser : SoftDeleteAuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public byte[]? ImageDataUrl { get; set; }
}
