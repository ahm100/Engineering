using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Persistence.Repositories.ProcesVerbals;

public class ProcesVerbalItemRepository : BaseRepository<EngineeringDBContext, ProcesVerbalItem>, IProcesVerbalItemRepository
{
    public ProcesVerbalItemRepository(EngineeringDBContext context) : base(context)
    {
    }
}
