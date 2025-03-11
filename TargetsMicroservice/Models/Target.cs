using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace TargetsMicroservice.Models;

public partial class Target
{
    public int RowId { get; set; }

    public long Targetid { get; set; }

    public Point Location { get; set; } = null!;

    public DateTime Detectiontime { get; set; }

    public string? Imagelink { get; set; }

    public string? Comment { get; set; }

    public int Targettypeid { get; set; }

    public long Flightid { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual Targettype Targettype { get; set; } = null!;
}
