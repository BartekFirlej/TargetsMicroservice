using System;
using System.Collections.Generic;

namespace TargetsMicroservice.Models;

public partial class Operator
{
    public int Operatorid { get; set; }

    public string? Name { get; set; }

    public string Key { get; set; } = null!;

    public int Teamid { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual ICollection<Reconarea> Reconareas { get; set; } = new List<Reconarea>();

    public virtual Team Team { get; set; } = null!;
}
