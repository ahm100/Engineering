using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Commands.ConfirmRequestReward;

public class ConfirmRequestRewardCommandHandler : ICommandHandler<ConfirmRequestRewardCommand, RequestReward>
{
    private readonly ILogger<ConfirmRequestRewardCommandHandler> _logger;
    private readonly IRequestRewardRepository _requestRewardRepository;

    public ConfirmRequestRewardCommandHandler(ILogger<ConfirmRequestRewardCommandHandler> logger, IRequestRewardRepository requestRewardRepository)
    {
        _logger = logger;
        _requestRewardRepository = requestRewardRepository;
    }

    public async Task<Result<RequestReward?>> Handle(ConfirmRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = await _requestRewardRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);
            if (entity.Status != RequestRewardStatus.Pending)
                return Result.Failure<RequestReward>(RequestRewardErrors.InValidRequestRewardStatus);

            entity.SetConfirmedPrice(request.ConfirmedPrice);
            entity.SetManagerDescription(request.ManagerDescription);
            if (request.CurrencyId is not null && request.CurrencyId > 0)
                entity.SetCurrencyId(request.CurrencyId!.Value);
            entity.Confirm();
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
