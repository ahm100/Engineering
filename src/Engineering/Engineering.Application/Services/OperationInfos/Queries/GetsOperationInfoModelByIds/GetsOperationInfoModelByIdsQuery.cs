using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoModelByIds;

public record GetsOperationInfoModelByIdsQuery(
    List<long> Ids,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetOperationInfosModel>>>;