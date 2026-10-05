using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.InactiveSeason;

public record InactiveSeasonCommand(
    long Id
    ) : ICommand<Season>;