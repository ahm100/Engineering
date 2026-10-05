using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationMachinery;

public class CreateDailyProjectOperationMachineryCommandHandler : ICommandHandler<CreateDailyProjectOperationMachineryCommand, DailyProjectOperationMachinery>
{
    private readonly ILogger<CreateDailyProjectOperationMachineryCommandHandler> _logger;
    private readonly IDailyProjectOperationMachineryRepository _repository;

    public CreateDailyProjectOperationMachineryCommandHandler(ILogger<CreateDailyProjectOperationMachineryCommandHandler> logger,
                                                              IDailyProjectOperationMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationMachinery?>> Handle(CreateDailyProjectOperationMachineryCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationMachinery(request.RequestMachinery,
                                                            request.FinalValue,
                                                            request.UnusedValue,
                                                            request.Number,
                                                            request.DailyProjectOperation,
                                                            request.ConsumableVolumeMachinery);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationMachinery?>(SharedErrors.UnknownError);
        }
    }
}
