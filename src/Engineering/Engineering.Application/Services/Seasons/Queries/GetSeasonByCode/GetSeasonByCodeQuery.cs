using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCode;

public record GetSeasonByCodeQuery(
    string SeasonCode,
    long BranchId,
    long? CompanyId
    ) : IQuery<Season>;