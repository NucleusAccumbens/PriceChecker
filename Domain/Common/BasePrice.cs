using Domain.Enums;

namespace Domain.Common;

public abstract class BasePrice : BaseAuditableEntity
{
    public Product Product { get; set; }

    public decimal ProductPrice { get; set; }

    public string? Amount { get; set; }

    public bool Availability { get; set; }
}
