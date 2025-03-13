using TargetsMicroservice.Models;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface ITargetTypesRepository
    {
        public Task<Targettype> GetTargetTypeByName(string typeName);
    }
}
