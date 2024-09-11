using System;
using System.Collections.Generic;

namespace Resturent2.Models;

public partial class Ruserlogin
{
    public decimal Id { get; set; }

    public string? UserName { get; set; }

    public string? Passwordd { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual Rcustomer? Customer { get; set; }

    public virtual Rrole? Role { get; set; }
}
