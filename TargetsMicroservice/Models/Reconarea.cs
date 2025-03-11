using System;
using System.Collections.Generic;

namespace TargetsMicroservice.Models;

public partial class Reconarea
{
    public int RowId { get; set; }

    public int Reconareaid { get; set; }

    public string Area { get; set; } = null!;

    public string Areashape { get; set; } = null!;

    public DateTime Periodfrom { get; set; }

    public DateTime Periodto { get; set; }

    public string? Comment { get; set; }

    public int? Operatorid { get; set; }

    public int? Teamid { get; set; }

    public int? Platoonid { get; set; }

    public virtual Operator? Operator { get; set; }

    public virtual Platoon? Platoon { get; set; }

    public virtual Team? Team { get; set; }
}
