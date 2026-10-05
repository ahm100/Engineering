using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsTotalProjectOperationReporting;

public record GetsTotalProjectOperationReportingQuery(
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
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsTotalProjectOperationReportingResponseModel>>>;