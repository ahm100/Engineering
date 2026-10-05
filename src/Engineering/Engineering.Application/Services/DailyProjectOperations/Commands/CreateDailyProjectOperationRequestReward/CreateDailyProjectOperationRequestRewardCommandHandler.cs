using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationRequestReward;

public class CreateDailyProjectOperationRequestRewardCommandHandler : ICommandHandler<CreateDailyProjectOperationRequestRewardCommand, DailyProjectOperationRequestReward>
{
    private readonly ILogger<CreateDailyProjectOperationRequestRewardCommandHandler> _logger;
    private readonly IDailyProjectOperationRequestRewardRepository _repository;

    public CreateDailyProjectOperationRequestRewardCommandHandler(ILogger<CreateDailyProjectOperationRequestRewardCommandHandler> logger, IDailyProjectOperationRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationRequestReward?>> Handle(CreateDailyProjectOperationRequestRewardCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationRequestReward(request.DailyProjectOperation.Id, request.RequestRewardId);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationRequestReward?>(SharedErrors.UnknownError);
        }
    }
}
