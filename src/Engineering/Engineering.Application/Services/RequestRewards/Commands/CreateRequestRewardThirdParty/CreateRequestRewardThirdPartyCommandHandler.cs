using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardThirdParty;

public class CreateRequestRewardThirdPartyCommandHandler : ICommandHandler<CreateRequestRewardThirdPartyCommand, RequestRewardThirdParty>
{
    private readonly ILogger<CreateRequestRewardThirdPartyCommandHandler> _logger;
    private readonly IRequestRewardThirdPartyRepository _repository;

    public CreateRequestRewardThirdPartyCommandHandler(ILogger<CreateRequestRewardThirdPartyCommandHandler> logger, IRequestRewardThirdPartyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestRewardThirdParty?>> Handle(CreateRequestRewardThirdPartyCommand request, CT ct)
    {
        try
        {
            RequestRewardThirdParty? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestRewardThirdParty>(RequestRewardThirdPartyErrors.RequestRewardThirdPartyNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.ThirdPartyId, request.RequestReward);
                }

                await _repository.Update(result);
            }
            else
            {
                var entity = new RequestRewardThirdParty(request.ThirdPartyId, request.RequestReward);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestRewardThirdParty>(SharedErrors.UnknownError);
        }
    }
}
