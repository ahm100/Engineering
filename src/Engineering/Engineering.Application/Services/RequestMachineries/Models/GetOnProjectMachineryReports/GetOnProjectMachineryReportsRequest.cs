using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReports;

public record GetOnProjectMachineryReportsRequest(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? MachineryIds,
    RequestMachineryUnit? Unit,
    List<RequestMachineryStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? ConfirmedFromDate,
    DateTime? ConfirmedToDate,
    string? FilterData,
    string[]? OrderBy,
    bool? OwnCompany,
    int PageIndex,
    int PageSize) : IHttpRequest;
