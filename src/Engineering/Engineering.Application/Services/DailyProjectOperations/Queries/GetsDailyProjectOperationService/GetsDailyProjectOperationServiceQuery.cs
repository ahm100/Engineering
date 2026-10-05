using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationService;

public record GetsDailyProjectOperationServiceQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ContractorIds,
    List<long>? ServiceInfoIds,
    List<long>? MeasurUnitIds,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? FromDate,
    DateTime? ToDate,
    ProjectOperationDetailStatus? Status,
    string? FilterData,
    string? FilterServiceInfo,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsDailyProjectOperationServiceModel>>>;
