
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetActiveSeasons;

public record GetActiveSeasonsQuery(
    string? FilterData,
    long? BranchId,
    string? SeasonCode,
    string? SeasonName,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;