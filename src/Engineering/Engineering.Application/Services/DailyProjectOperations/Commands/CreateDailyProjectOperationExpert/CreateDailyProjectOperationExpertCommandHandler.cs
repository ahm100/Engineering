using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationExpert;

public class CreateDailyProjectOperationExpertCommandHandler : ICommandHandler<CreateDailyProjectOperationExpertCommand, DailyProjectOperationExpert>
{
    private readonly ILogger<CreateDailyProjectOperationExpertCommandHandler> _logger;
    private readonly IDailyProjectOperationExpertRepository _repository;

    public CreateDailyProjectOperationExpertCommandHandler(ILogger<CreateDailyProjectOperationExpertCommandHandler> logger,
                                                           IDailyProjectOperationExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationExpert?>> Handle(CreateDailyProjectOperationExpertCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationExpert(request.ThirdPartyId,
                                                         request.FinalValue,
                                                         request.UnusedValue,
                                                         request.DailyProjectOperation,
                                                         request.ConsumableVolumeExpert);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationExpert?>(SharedErrors.UnknownError);
        }
    }
}
