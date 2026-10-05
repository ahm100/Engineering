using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonById;

public record GetSeasonByIdQuery(
    long Id
    ) : IQuery<Season>;