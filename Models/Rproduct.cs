using System;
using System.Collections.Generic;

namespace Resturent2.Models;

public partial class Rproduct
{
    public decimal Id { get; set; }

    public string? Namee { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? CategoryId { get; set; }

    public virtual Rcategory? Category { get; set; }

    public virtual ICollection<Rproductcustomer> Rproductcustomers { get; set; } = new List<Rproductcustomer>();
}
