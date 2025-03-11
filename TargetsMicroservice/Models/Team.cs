using System;
using System.Collections.Generic;

namespace TargetsMicroservice.Models;

public partial class Team
{
    public int Teamid { get; set; }

    public string? Name { get; set; }

    public int Platoonid { get; set; }

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();

    public virtual Platoon Platoon { get; set; } = null!;

    public virtual ICollection<Reconarea> Reconareas { get; set; } = new List<Reconarea>();
}
