using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByIds;

public record GetsOperationInfoServiceByIdsQuery(
    List<long> Ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoService>>>;