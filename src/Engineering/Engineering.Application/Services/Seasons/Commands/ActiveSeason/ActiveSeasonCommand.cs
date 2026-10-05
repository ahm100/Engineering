using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.ActiveSeason;

public record ActiveSeasonCommand(
    long Id
    ) : ICommand<Season>;