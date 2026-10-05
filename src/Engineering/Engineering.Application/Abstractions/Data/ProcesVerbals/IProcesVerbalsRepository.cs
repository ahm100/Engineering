using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Abstractions.Data.ProcesVerbals;

public interface IProcesVerbalsRepository : IBaseRepository<ProcesVerbal, long>
{
    Task<ProcesVerbal?> GetById(long Id);

    Task<(List<GetProcesVerbalsResponseModel> Data, int RowCount)> GetProcesVerbals(
            long? targetId, long? projectId, string? title,
            long? contractId, ProcesVerbalType? type,
            int pageIndex, int pageSize, CT ct);

    Task<GetProcesVerbalDetailByIdResponse?> GetProcesVerbalDetailById(
        long id, CT ct);
}
