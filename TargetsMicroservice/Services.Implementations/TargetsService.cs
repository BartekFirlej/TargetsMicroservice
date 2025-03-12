using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class TargetsService : ITargetsService
    {
        private readonly ITargetsRepository _targetsRepository;
        private readonly IPhotoUploadService _photoUploadService;

        public TargetsService(ITargetsRepository targetsRepository, IPhotoUploadService photoUploadService)
        {
            _targetsRepository = targetsRepository;
            _photoUploadService = photoUploadService;
        }

        public async Task<TargetResponse> AddTarget(TargetRequest target)
        {
            if (target.Image != null)
            {
                var image = Convert.FromBase64String(target.Image);
                target.Image = await _photoUploadService.UploadPhoto(image, target.Targetid, target.Detectiontime);
            }
            var addedTarget = await _targetsRepository.AddTarget(target);
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
