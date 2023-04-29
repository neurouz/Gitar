using Gitar.Domain.Contracts.Data;

namespace Gitar.Domain.Common;

public class AuditableEntity : IEntityBase
{
    public DateTime CreateDate { get; set; }
    public DateTime ModifyDate { get; set; }
}
