using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalDailyProjectOperationService;

public record GetsTotalDailyProjectOperationServiceQuery(
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
    string? FilterServiceInfo
    ) : IQuery<List<GetsTotalDailyProjectOperationServiceModel>>;
