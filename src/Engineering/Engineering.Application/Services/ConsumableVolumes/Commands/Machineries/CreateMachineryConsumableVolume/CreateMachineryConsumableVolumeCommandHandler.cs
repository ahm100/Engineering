using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.CreateMachineryConsumableVolume;

public class CreateConsumableVolumeMachineryCommandHandler : ICommandHandler<CreateConsumableVolumeMachineryCommand, ConsumableVolumeMachinery>
{
    private readonly ILogger<CreateConsumableVolumeMachineryCommand> _logger;
    private readonly IConsumableVolumeMachineryRepository _repository;

    public CreateConsumableVolumeMachineryCommandHandler(ILogger<CreateConsumableVolumeMachineryCommand> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeMachinery?>> Handle(CreateConsumableVolumeMachineryCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumableVolumeMachinery(request.ProjectOperationDetail, request.Machinery, request.Number ?? 0,
                request.UnusedPercentage, request.IsStandard, request.StandardValue, request.FinalValue, request.Unit);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeMachinery>(SharedErrors.UnknownError);
        }
    }
}