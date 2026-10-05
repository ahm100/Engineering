using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;

public record GetFilteredRequestMachineryStatusStatementRequest(
        List<long>? ContractorIds,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? MachineryIds,
        List<RequestMachineryStatusStatementStatus>? Statuses,
        RequestMachineryStatusStatementUnit? Unit,
        DateTime? StartDate,
        DateTime? EndDate,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize
    ) : IHttpRequest;
