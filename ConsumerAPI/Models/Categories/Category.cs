using System;
using System.Collections.Generic;
using ConsumerAPI.Models.Products;

namespace ConsumerAPI.Models.Categories;

public partial class Category
{
    public long Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
