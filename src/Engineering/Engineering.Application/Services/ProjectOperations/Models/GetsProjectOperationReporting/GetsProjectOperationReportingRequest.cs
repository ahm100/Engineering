using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;

public record GetsProjectOperationReportingRequest(
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
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
