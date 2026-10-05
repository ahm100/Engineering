using Engineering.Domain.Entities.Branchs;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.CreateSeason;

public record CreateSeasonCommand(
    Branch Branch,
    string SeasonCode,
    string SeasonName,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Season>;