using Gitar.Domain.Common;

namespace Gitar.Domain.Models;

public class GitUser : SoftDeleteAuditableEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public byte[]? ImageDataUrl { get; set; }
}
