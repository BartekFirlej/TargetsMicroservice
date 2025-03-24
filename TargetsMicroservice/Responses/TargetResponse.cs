namespace TargetsMicroservice.Responses
{
    public class TargetResponse
    {
        public long Targetid { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public DateTime Detectiontime { get; set; }

        public string? Imagelink { get; set; }

        public string? Comment { get; set; }

        public int Targettypeid { get; set; }

        public string Targettypename { get; set; }

        public long Flightid { get; set; }

        public int Operatorid { get; set; }

        public string Operatorname { get; set; }

        public int Teamid { get; set; }

        public string Teamname { get; set; }

        public int Platoonid { get; set; }

        public string Platoonname { get; set; }

    }
}
