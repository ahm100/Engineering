using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDetailedDailyOperationTotals;

public record GetDetailedDailyOperationTotalsQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ContractorIds,
    List<long>? ServiceInfoIds,
    List<long>? CreatorIds,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? FromDate,
    DateTime? ToDate,
    List<ProjectOperationDetailStatus>? Status,
    List<ProjectOperationDetailStatus>? ProjectOperationDetailStatus,
    string? FilterData
    ) : IQuery<List<GetDetailedDailyProjectOperationTotalsModel>>;
