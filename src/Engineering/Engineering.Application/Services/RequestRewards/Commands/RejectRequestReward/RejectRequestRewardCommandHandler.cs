using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Commands.RejectRequestReward;

public class RejectRequestRewardCommandHandler : ICommandHandler<RejectRequestRewardCommand, RequestReward?>
{
    private readonly IRequestRewardRepository _requestRewardRepository;
    private readonly ILogger<RejectRequestRewardCommandHandler> _logger;

    public RejectRequestRewardCommandHandler(ILogger<RejectRequestRewardCommandHandler> logger, IRequestRewardRepository requestRewardRepository)
    {
        _requestRewardRepository = requestRewardRepository;
        _logger = logger;
    }

    public async Task<Result<RequestReward?>> Handle(RejectRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = await _requestRewardRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);
            if (entity.Status != RequestRewardStatus.Pending)
                return Result.Failure<RequestReward>(RequestRewardErrors.InValidRequestRewardStatus);

            entity.SetManagerDescription(request.ManagerDescription);
            entity.Reject();
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
