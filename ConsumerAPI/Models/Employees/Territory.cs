using System;
using System.Collections.Generic;

namespace ConsumerAPI.Models.Employees;

public partial class Territory
{
    public string Id { get; set; } = null!;

    public string? TerritoryDescription { get; set; }

    public long RegionId { get; set; }

    public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; } = new List<EmployeeTerritory>();
}
