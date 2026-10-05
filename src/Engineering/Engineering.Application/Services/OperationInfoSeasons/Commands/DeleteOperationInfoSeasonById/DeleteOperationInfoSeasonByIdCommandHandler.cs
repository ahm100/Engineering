using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeasonById;

public class DeleteOperationInfoSeasonByIdCommandHandler : ICommandHandler<DeleteOperationInfoSeasonByIdCommand, OperationInfoSeason>
{
    private readonly ILogger<DeleteOperationInfoSeasonByIdCommand> _logger;
    private readonly IOperationInfoSeasonRepository _repository;

    public DeleteOperationInfoSeasonByIdCommandHandler(ILogger<DeleteOperationInfoSeasonByIdCommand> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoSeason?>> Handle(DeleteOperationInfoSeasonByIdCommand request, CT ct)
    {
        try
        {
            var opSeason = await _repository.GetOperationInfoSeasonById(request.Id, ct);
            if (opSeason is null)
                return Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.OperationInfoSeasonWithIdNotFound);
            if (opSeason.IsDeleted == true)
                return Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.IsDeleted);
            if (opSeason.RequestGoodsSupplies.Count > 0)
                return Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.HaveRequestGoodsSupply);

            var operationInfoId = opSeason!.OperationInfo.Id;
            var seasonId = opSeason!.Season.Id;
            var entities = await _repository.GetBySeasonAndOI(seasonId, operationInfoId, ct);
            foreach (var entity in entities)
            {
                entity.SetIsDeleted();
                await _repository.Update(entity);
            }
            return opSeason;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoSeason>(SharedErrors.UnknownError);
        }
    }
}