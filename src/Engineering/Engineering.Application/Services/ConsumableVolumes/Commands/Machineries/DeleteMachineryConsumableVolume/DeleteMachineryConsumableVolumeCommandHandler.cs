using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.DeleteMachineryConsumableVolume;

public class DeleteConsumableVolumeMachineryCommandHandler : ICommandHandler<DeleteConsumableVolumeMachineryCommand, ConsumableVolumeMachinery>
{
    private readonly ILogger<DeleteConsumableVolumeMachineryCommand> _logger;
    private readonly IConsumableVolumeMachineryRepository _repository;

    public DeleteConsumableVolumeMachineryCommandHandler(ILogger<DeleteConsumableVolumeMachineryCommand> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeMachinery?>> Handle(DeleteConsumableVolumeMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumableVolumeMachinery>(ConsumableVolumeMachineryErrors.ProjectOperationDetailWithIdNotFound);
            if (entity.IsStandard)
                return Result.Failure<ConsumableVolumeMachinery>(ConsumableVolumeMachineryErrors.DataIsStandard);

            entity.SetIsDeleted();
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
