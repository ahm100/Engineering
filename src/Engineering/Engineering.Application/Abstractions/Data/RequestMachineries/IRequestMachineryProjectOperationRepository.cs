using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryProjectOperationRepository : IBaseRepository<RequestMachineryProjectOperation>
{
    Task<(List<RequestMachineryProjectOperation> Data, int RowCount)> GetFilteredProjectOperation(long RequestMachineryId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct);
}
