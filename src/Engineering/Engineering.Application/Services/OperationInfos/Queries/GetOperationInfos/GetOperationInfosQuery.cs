using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfos;

public record GetOperationInfosQuery(
    List<long> Ids
    ) : IQuery<List<OperationInfo>>;