
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetBySeasonIdsIncludeLess;

public record GetBySeasonIdsIncludeLessQuery(
    List<long> SeasonIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;