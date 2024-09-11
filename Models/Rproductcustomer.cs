using System;
using System.Collections.Generic;

namespace Resturent2.Models;

public partial class Rproductcustomer
{
    public decimal Id { get; set; }

    public decimal? ProductId { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public virtual Rcustomer? Customer { get; set; }

    public virtual Rproduct? Product { get; set; }
}
