using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationRequestReward;

public class DeleteDailyProjectOperationRequestRewardCommandHandler : ICommandHandler<DeleteDailyProjectOperationRequestRewardCommand, DailyProjectOperationRequestReward>
{
    private readonly ILogger<DeleteDailyProjectOperationRequestRewardCommandHandler> _logger;
    private readonly IDailyProjectOperationRequestRewardRepository _repository;

    public DeleteDailyProjectOperationRequestRewardCommandHandler(ILogger<DeleteDailyProjectOperationRequestRewardCommandHandler> logger,
                                                           IDailyProjectOperationRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationRequestReward?>> Handle(DeleteDailyProjectOperationRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DeleteDailyProjectOperationRequestRewardId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationRequestReward?>(DailyProjectOperationRequestRewardErrors.DailyProjectOperationRequestRewardWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationRequestReward?>(DailyProjectOperationRequestRewardErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationRequestReward?>(SharedErrors.UnknownError);
        }
    }
}
