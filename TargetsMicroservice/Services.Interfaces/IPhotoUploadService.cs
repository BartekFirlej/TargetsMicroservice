namespace TargetsMicroservice.Services.Interfaces
{
    public interface IPhotoUploadService
    {
        public Task<string> UploadPhoto(byte[] image, long targetID, DateTime detectionTime);
    }
}
