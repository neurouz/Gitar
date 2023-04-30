namespace Gitar.Domain.Common;

public class SoftDeleteAuditableEntity : AuditableEntity
{
    public bool IsActive { get; set; } = true;
}
