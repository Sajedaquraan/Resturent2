using System;
using System.Collections.Generic;

namespace Resturent2.Models;

public partial class Rrole
{
    public decimal Id { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<Ruserlogin> Ruserlogins { get; set; } = new List<Ruserlogin>();
}
