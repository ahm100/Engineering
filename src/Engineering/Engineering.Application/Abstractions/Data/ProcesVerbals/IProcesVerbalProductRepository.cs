using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Application.Abstractions.Data.ProcesVerbals;

public interface IProcesVerbalProductRepository : IBaseRepository<ProcesVerbalProduct, long>
{
    Task<List<ProcesVerbalProduct>> GetProcesVerbalProductByIds(List<long> Ids);
}
