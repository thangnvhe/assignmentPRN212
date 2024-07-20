using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
