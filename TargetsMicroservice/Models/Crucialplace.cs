using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace TargetsMicroservice.Models;

public partial class Crucialplace
{
    public int RowId { get; set; }

    public int Crucialplaceid { get; set; }

    public Point Location { get; set; } = null!;

    public string? Comment { get; set; }
}
