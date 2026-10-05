using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyServiceInfo;

public record GetsDailyServiceInfoQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ContractorIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsDailyServiceInfoModel>>>;
