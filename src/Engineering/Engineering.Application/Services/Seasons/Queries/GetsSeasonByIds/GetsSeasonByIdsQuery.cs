
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetsSeasonByIds;

public record GetsSeasonByIdsQuery(
    List<long> SeasonIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;