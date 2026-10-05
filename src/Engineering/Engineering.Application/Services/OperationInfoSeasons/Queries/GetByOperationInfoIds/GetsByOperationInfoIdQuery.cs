using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetByOperationInfoIdsQuery;

public record GetByOperationInfoIdsQuery(
    List<long>? OprationInfoIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoSeason>>>;
