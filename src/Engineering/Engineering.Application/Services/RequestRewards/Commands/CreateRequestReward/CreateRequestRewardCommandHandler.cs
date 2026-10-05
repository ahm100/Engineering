using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestReward;

public class CreateRequestRewardCommandHandler : ICommandHandler<CreateRequestRewardCommand, RequestReward>
{
    private readonly ILogger<CreateRequestRewardCommandHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public CreateRequestRewardCommandHandler(ILogger<CreateRequestRewardCommandHandler> logger, IRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestReward?>> Handle(CreateRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = new RequestReward(request.OfferedPrice, request.Description, request.RegistrationDate, request.Type, request.CurrencyId, request.CostCenter,
                request.Project, request.ProjectOperation, request.ProjectOperationDetail, request.FiduciaryProductDetailReturn, request.CompanyId);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestReward>(SharedErrors.UnknownError);
        }

    }
}
