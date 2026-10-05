using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoHistoryById;

public record GetsOperationInfoHistoryByIdQuery(
    long OperationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsOperationInfoHistoryByIdModel>>>;
