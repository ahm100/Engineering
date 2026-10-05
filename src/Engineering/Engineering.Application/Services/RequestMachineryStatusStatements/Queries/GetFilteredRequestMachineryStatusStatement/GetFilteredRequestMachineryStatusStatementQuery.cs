using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetFilteredRequestMachineryStatusStatement;

public record GetFilteredRequestMachineryStatusStatementQuery(
        List<long>? Ids,
        List<long>? ContractorIds,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? MachineryIds,
        List<RequestMachineryStatusStatementStatus>? Statuses,
        RequestMachineryStatusStatementUnit? Unit,
        DateTime? StartDate,
        DateTime? EndDate,
        long? CompanyId,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<RequestMachineryStatusStatement>>>;
