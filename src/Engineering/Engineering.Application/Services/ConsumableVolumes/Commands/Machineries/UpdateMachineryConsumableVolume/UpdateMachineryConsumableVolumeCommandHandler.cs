using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.UpdateMachineryConsumableVolume;

public class UpdateConsumableVolumeMachineryCommandHandler : ICommandHandler<UpdateConsumableVolumeMachineryCommand, ConsumableVolumeMachinery>
{
    private readonly ILogger<UpdateConsumableVolumeMachineryCommand> _logger;
    private readonly IConsumableVolumeMachineryRepository _repository;

    public UpdateConsumableVolumeMachineryCommandHandler(ILogger<UpdateConsumableVolumeMachineryCommand> logger,
        IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeMachinery?>> Handle(UpdateConsumableVolumeMachineryCommand request, CT ct)
    {
        try
        {
            var entity = request.ConsumableVolumeMachinery;
            entity.SetNumber(request.Number ?? 0);
            entity.SetUnusedPercentage(request.UnusedPercentage);
            entity.SetIsStandard(request.IsStandard);
            entity.SetStandardValue(request.StandardValue);
            entity.SetFinalValue(request.FinalValue);
            entity.SetMachinery(request.Machinery);
            entity.SetUnit(request.Unit);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeMachinery>(SharedErrors.UnknownError);
        }
    }
}