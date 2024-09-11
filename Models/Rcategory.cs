using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturent2.Models;

public partial class Rcategory
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }


    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    public virtual ICollection<Rproduct> Rproducts { get; set; } = new List<Rproduct>();
}
