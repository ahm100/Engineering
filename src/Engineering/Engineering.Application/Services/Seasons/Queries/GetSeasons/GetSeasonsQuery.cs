
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasons;

public record GetSeasonsQuery(
    List<long>? Ids,
    string? FilterData,
    long? BranchId,
    long? CategoryId,
    string? SeasonCode,
    string? SeasonName,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;