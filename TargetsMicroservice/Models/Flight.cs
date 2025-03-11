using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace TargetsMicroservice.Models;

public partial class Flight
{
    public long Flightid { get; set; }

    public DateTime Begintime { get; set; }

    public DateTime? Endtime { get; set; }

    public Point? Beginpoint { get; set; }

    public string? Comment { get; set; }

    public int Operatorid { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual ICollection<Target> Targets { get; set; } = new List<Target>();
}
