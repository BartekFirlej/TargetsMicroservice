namespace TargetsMicroservice.Responses
{
    public class FlightResponse
    {
        public long Flightid { get; set; }

        public DateTime Begintime { get; set; }

        public DateTime? Endtime { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public string? Comment { get; set; }

        public int Operatorid { get; set; }

        public string? VideoStream { get; set; }
    }
}
