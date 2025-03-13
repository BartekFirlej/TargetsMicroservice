using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class TargetsService : ITargetsService
    {
        private readonly ITargetsRepository _targetsRepository;
        private readonly IPhotoUploadService _photoUploadService;
        private readonly ITargetTypesService _targetTypesService;

        public TargetsService(ITargetsRepository targetsRepository, IPhotoUploadService photoUploadService, ITargetTypesService targetTypesService)
        {
            _targetsRepository = targetsRepository;
            _photoUploadService = photoUploadService;
            _targetTypesService = targetTypesService;
        }

        public async Task<TargetResponse> AddTarget(TargetRequest target)
        {
            string imageLink ="";
            if (target.Image != null)
            {
                imageLink = await _photoUploadService.UploadPhoto(target.Image, target.ObjectId, target.Timestamp);
            }
            var targetType = await _targetTypesService.GetTargetTypeByName(target.ObjectCategory);
            var targetDBRequest = new TargetDatabaseRequest
            {
                Flightid = target.FlightId,
                Comment = target.Comment,
                Detectiontime = target.Timestamp,
                Image = target.Image != null ? imageLink : null,
                Targetid = target.ObjectId,
                Targettypeid = targetType.Targettypeid,
                X = target.X,
                Y = target.Y,
                Z = target.Z
            };
            var addedTarget = await _targetsRepository.AddTarget(targetDBRequest);
            return await GetTargetById(addedTarget.Targetid);
        }

        public async Task<TargetResponse> GetTargetById(long targetId)
        {
            return await _targetsRepository.GetTargetById(targetId);
        }

        public async Task<List<TargetResponse>> GetTargets()
        {
            return await _targetsRepository.GetTargets();
        }
    }
}
