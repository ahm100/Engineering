using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsOperationInfoSeasonByProjectOperationId;

public record GetsOperationInfoSeasonByProjectOperationIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoSeason>>>;
