using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.DisableSeason;

public record DisableSeasonCommand(
    long Id
    ) : ICommand<Season>;