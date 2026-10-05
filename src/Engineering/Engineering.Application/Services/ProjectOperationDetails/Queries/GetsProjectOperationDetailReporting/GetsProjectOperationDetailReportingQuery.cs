using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailReporting;

public record GetsProjectOperationDetailReportingQuery(
    List<long>? Ids,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? CreateFrom,
    DateTime? CreateTo,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ContractorIds,
    List<ProjectOperationDetailStatus>? Statuses,
    string? LocationFilterData,
    string? DescriptionFilterData,
    string? DailyDescription,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsProjectOperationDetailReportingModel>>>;