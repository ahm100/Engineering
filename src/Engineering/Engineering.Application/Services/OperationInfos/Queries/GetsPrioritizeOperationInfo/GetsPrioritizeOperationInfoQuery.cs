
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsPrioritizeOperationInfo;

public record GetsPrioritizeOperationInfoQuery(
    string? FilterData,
    long? Id,
    int Priority,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;