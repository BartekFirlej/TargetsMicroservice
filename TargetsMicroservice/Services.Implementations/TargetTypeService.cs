using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class TargetTypeService : ITargetTypesService
    {
        private readonly ITargetTypesRepository _targetTypeRepository;

        public TargetTypeService(ITargetTypesRepository targetTypeRepository)
        {
            _targetTypeRepository = targetTypeRepository;
        }

        public async Task<Targettype> GetTargetTypeByName(string typeName)
        {
            return await _targetTypeRepository.GetTargetTypeByName(typeName);
        }
    }
}
