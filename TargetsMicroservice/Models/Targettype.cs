using System;
using System.Collections.Generic;

namespace TargetsMicroservice.Models;

public partial class Targettype
{
    public int Targettypeid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Target> Targets { get; set; } = new List<Target>();
}
