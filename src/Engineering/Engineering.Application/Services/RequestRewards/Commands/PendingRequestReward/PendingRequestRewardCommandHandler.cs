using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Commands.PendingRequestReward;

public class PendingRequestRewardCommandHandler : ICommandHandler<PendingRequestRewardCommand, RequestReward?>
{
    private readonly IRequestRewardRepository _requestRewardRepository;
    private readonly ILogger<PendingRequestRewardCommandHandler> _logger;

    public PendingRequestRewardCommandHandler(ILogger<PendingRequestRewardCommandHandler> logger, IRequestRewardRepository requestRewardRepository)
    {
        _requestRewardRepository = requestRewardRepository;
        _logger = logger;
    }

    public async Task<Result<RequestReward?>> Handle(PendingRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = await _requestRewardRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);
            if (entity.Status != RequestRewardStatus.New)
                return Result.Failure<RequestReward>(RequestRewardErrors.InValidRequestRewardStatus);

            entity.Pending();
            entity.AddHistory();

            await _requestRewardRepository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestReward>(SharedErrors.UnknownError);
        }
    }
}
