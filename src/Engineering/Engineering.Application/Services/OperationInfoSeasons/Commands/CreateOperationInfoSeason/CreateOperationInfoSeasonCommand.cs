using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.OperationInfoSeasons.Commands.CreateOperationInfoSeason;

public record CreateOperationInfoSeasonCommand(
    OperationInfo OperationInfo,
    List<Season> Seasons
    ) : ICommand<List<OperationInfoSeason>?>;
