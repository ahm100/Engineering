using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationReporting;

public record GetsProjectOperationReportingQuery(
    List<long>? Ids,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ContractorIds,
    List<ProjectOperationStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    string? Description,
    string? DailyDescription,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsProjectOperationReportingModel>>>;