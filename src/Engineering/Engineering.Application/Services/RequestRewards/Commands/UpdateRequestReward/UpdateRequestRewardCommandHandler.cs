using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.UpdateRequestReward;

public class UpdateRequestRewardCommandHandler : ICommandHandler<UpdateRequestRewardCommand, RequestReward>
{
    private readonly ILogger<UpdateRequestRewardCommandHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public UpdateRequestRewardCommandHandler(ILogger<UpdateRequestRewardCommandHandler> logger, IRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestReward?>> Handle(UpdateRequestRewardCommand request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            if (result is null)
                return Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);

            result.SetData(request.OfferedPrice, request.Description, request.RegistrationDate, request.Type, request.CurrencyId, request.CostCenter, request.Project,
                request.ProjectOperation, request.ProjectOperationDetail, request.FiduciaryProductDetailReturn, result.CompanyId);

            result.AddHistory();

            await _repository.Update(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestReward>(SharedErrors.UnknownError);
        }

    }
}
