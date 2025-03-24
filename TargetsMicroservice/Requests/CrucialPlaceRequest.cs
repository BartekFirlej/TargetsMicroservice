namespace TargetsMicroservice.Requests
{
    public class CrucialPlaceRequest
    {
        public int ObjectId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string? Comment { get; set; } = null!;
    }
}
