namespace TargetsMicroservice.Requests
{
    public class ReconAreaRequest
    {
        public int Reconareaid { get; set; }

        public string Area { get; set; } = null!;

        public string Areashape { get; set; } = null!;

        public DateTime Periodfrom { get; set; }

        public DateTime Periodto { get; set; }

        public string? Comment { get; set; }

        public int? Operatorid { get; set; }

        public int? Teamid { get; set; }

        public int? Platoonid { get; set; }
    }
}
