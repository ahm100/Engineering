using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.StateChangerSeasons;

public record StateChangerSeasonsCommand(
    List<Season> Items,
    bool State
    ) : ICommand<bool?>;
