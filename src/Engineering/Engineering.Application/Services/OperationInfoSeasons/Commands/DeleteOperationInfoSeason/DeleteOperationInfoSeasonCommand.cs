using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeason;

public record DeleteOperationInfoSeasonCommand(
    long SeasonId,
    long OprationInfoId
    ) : ICommand<OperationInfoSeason>;
