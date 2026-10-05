using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Persistence.Repositories.ProcesVerbals;

public class ProcesVerbalProductRepository : BaseRepository<EngineeringDBContext, ProcesVerbalProduct>, IProcesVerbalProductRepository
{
    public ProcesVerbalProductRepository(EngineeringDBContext context): base(context)
    {
    }

    public async Task<List<ProcesVerbalProduct>> GetProcesVerbalProductByIds(List<long> Ids)
        => await DbSet.Where(e => Ids.Contains(e.Id)).ToListAsync();
}
