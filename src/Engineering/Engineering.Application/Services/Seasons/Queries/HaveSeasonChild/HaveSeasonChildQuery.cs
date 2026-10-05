using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.HaveSeasonChild;

public record HaveSeasonChildQuery(
    long Id
    ) : IQuery<Season>;