using Minio;
using Minio.DataModel.Args;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly IMinioClient _minioClient;
        private readonly string _BUCKET_NAME;
        private readonly string _MINIO_URI;

        public PhotoUploadService(IMinioClient minioClient, string BUCKET_NAME, string MINIO_URI)
        {
            _minioClient = minioClient;
            _BUCKET_NAME = BUCKET_NAME;
            _MINIO_URI = MINIO_URI;
        }

        public async Task<string> UploadPhoto(byte[] image, long targetID, DateTime detectionTime)
        {
            var objectName = targetID.ToString() + "_" + detectionTime.ToString("yyyyMMddHHmmss");
            using (var memoryStream = new MemoryStream(image))
            {
                await _minioClient.PutObjectAsync(
                new PutObjectArgs()
                        .WithBucket(_BUCKET_NAME)
                        .WithObject(objectName)
                        .WithStreamData(memoryStream)
                        .WithObjectSize(memoryStream.Length)
                        .WithContentType("image/png")
                );
            }
            string photoUrl = $"{_MINIO_URI}/{_BUCKET_NAME}/{objectName}";
            return photoUrl;
        }
    }
}
