﻿using System;
using System.Collections.Generic;
using ConsumerAPI.Models.Customers;
using ConsumerAPI.Models.Employees;
using ConsumerAPI.Models.Orders;

namespace ConsumerAPI.Models.Orders;

public partial class Order
{
    public long Id { get; set; }

    public string? OrderDate { get; set; }

    public string? RequiredDate { get; set; }

    public string? ShippedDate { get; set; }

    public long? ShipVia { get; set; }

    public byte[] Freight { get; set; } = null!;

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public string? CustomerId { get; set; }

    public long EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
