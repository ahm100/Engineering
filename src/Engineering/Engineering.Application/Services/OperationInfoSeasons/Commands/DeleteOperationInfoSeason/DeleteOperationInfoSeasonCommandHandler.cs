using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeason;

public class DeleteOperationInfoSeasonCommandHandler : ICommandHandler<DeleteOperationInfoSeasonCommand, OperationInfoSeason>
{
    private readonly ILogger<DeleteOperationInfoSeasonCommand> _logger;
    private readonly IOperationInfoSeasonRepository _repository;

    public DeleteOperationInfoSeasonCommandHandler(ILogger<DeleteOperationInfoSeasonCommand> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoSeason?>> Handle(DeleteOperationInfoSeasonCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.SeasonId, request.OprationInfoId, ct);
            if (entity is null)
                return Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.OperationInfoSeasonWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoSeason>(SharedErrors.UnknownError);
        }
    }
}