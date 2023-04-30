using Gitar.Domain.Contracts.Data;

namespace Gitar.Domain.Common;

public class AuditableEntity : IEntityBase
{
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime ModifyDate { get; set; } = DateTime.Now;
}
