using Microsoft.EntityFrameworkCore;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class TargetTypeRepository : ITargetTypesRepository
    {
        private readonly MagisterkaContext _dbContext;

        public TargetTypeRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Targettype> GetTargetTypeByName(string typeName)
        {
            return await _dbContext.Targettypes.Where(t => t.Name== typeName).FirstOrDefaultAsync();
        }
    }
}
