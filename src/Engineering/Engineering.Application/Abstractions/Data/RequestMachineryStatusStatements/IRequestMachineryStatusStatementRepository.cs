using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;

public interface IRequestMachineryStatusStatementRepository : IBaseRepository<RequestMachineryStatusStatement>
{
    Task<RequestMachineryStatusStatement?> GetRequestMachineryStatusStatementById(
        long id,
        CT ct);

    Task<RequestMachineryStatusStatement?> GetStatusStatementById(long id, CT ct);

    Task<(List<RequestMachineryStatusStatement> Data, int RowCount)> GetFilteredRequestMachineryStatusStatement(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? machineryIds,
        List<RequestMachineryStatusStatementStatus>? statuses,
        RequestMachineryStatusStatementUnit? unit,
        DateTime? startDate,
        DateTime? endDate,
        long? companyId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<RequestMachineryStatusStatement?> GetLastRequestMachineryStatusStatement(
        long contractorId,
        long? companyId,
        CT ct);
}
