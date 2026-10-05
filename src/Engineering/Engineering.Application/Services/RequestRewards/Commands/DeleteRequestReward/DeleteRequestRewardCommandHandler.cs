using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestReward;

public class DeleteRequestRewardCommandHandler : ICommandHandler<DeleteRequestRewardCommand, RequestReward>
{
    private readonly ILogger<DeleteRequestRewardCommandHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public DeleteRequestRewardCommandHandler(ILogger<DeleteRequestRewardCommandHandler> logger, IRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestReward?>> Handle(DeleteRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestReward>(RequestRewardErrors.IsDeleted);

            entity.SetIsDeleted();
            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestReward>(SharedErrors.UnknownError);
        }

    }
}
