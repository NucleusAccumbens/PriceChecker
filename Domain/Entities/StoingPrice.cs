using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class StoingPrice : BasePrice
{
    public string? AvailbleInMoskow { get; set; }

    public string? AvailbleInSpb { get; set; }
}
