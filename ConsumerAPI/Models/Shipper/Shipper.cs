using System;
using System.Collections.Generic;

namespace ConsumerAPI.Models.Shipper;

public partial class Shipper
{
    public long Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Phone { get; set; }
}
