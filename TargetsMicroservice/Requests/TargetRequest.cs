using NetTopologySuite.Geometries;

namespace TargetsMicroservice.Requests
{
    public class TargetRequest
    {
        public long Targetid { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public DateTime Detectiontime { get; set; }

        public string? Image { get; set; }

        public string? Comment { get; set; }

        public int Targettypeid { get; set; }

        public long Flightid { get; set; }
    }
}
