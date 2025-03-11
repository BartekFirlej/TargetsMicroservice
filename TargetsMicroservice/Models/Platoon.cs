using System;
using System.Collections.Generic;

namespace TargetsMicroservice.Models;

public partial class Platoon
{
    public int Platoonid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Reconarea> Reconareas { get; set; } = new List<Reconarea>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
