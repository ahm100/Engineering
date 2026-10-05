using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByName;

public record GetSeasonByNameQuery(
    string SeasonName,
    long BranchId,
    long? CompanyId
    ) : IQuery<Season>;