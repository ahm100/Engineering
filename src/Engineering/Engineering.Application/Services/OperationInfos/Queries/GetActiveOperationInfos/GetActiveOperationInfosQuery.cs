
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfos;

public record GetActiveOperationInfosQuery(
    string? FilterData,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    int? Priority,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;