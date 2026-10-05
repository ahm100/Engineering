using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;

public record GetsTotalProjectOperationReportingRequest(
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ContractorIds,
    List<ProjectOperationStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? MinimumWorkload,
    decimal? MinimumDoneWorkload,
    decimal? MinimumRemaindedWorkload,
    string? Description,
    string? DailyDescription,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
