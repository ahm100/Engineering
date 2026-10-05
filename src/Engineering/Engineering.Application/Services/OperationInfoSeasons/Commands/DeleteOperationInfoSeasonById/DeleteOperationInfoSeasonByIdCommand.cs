using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeasonById;

public record DeleteOperationInfoSeasonByIdCommand(
    long Id
    ) : ICommand<OperationInfoSeason>;
