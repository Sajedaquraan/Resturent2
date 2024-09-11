using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturent2.Models;

public partial class Rcustomer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public virtual ICollection<Rproductcustomer> Rproductcustomers { get; set; } = new List<Rproductcustomer>();

    public virtual ICollection<Ruserlogin> Ruserlogins { get; set; } = new List<Ruserlogin>();
}
