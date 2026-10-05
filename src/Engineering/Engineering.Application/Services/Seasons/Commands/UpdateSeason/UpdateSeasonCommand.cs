using Engineering.Domain.Entities.Branchs;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.UpdateSeason;

public record UpdateSeasonCommand(
    long Id,
    Branch Branch,
    string SeasonCode,
    string SeasonName,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Season>;