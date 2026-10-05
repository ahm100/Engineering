using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.DeleteExpertConsumableVolume;

public class DeleteConsumableVolumeExpertCommandHandler : ICommandHandler<DeleteConsumableVolumeExpertCommand, ConsumableVolumeExpert>
{
    private readonly ILogger<DeleteConsumableVolumeExpertCommand> _logger;
    private readonly IConsumableVolumeExpertRepository _repository;

    public DeleteConsumableVolumeExpertCommandHandler(ILogger<DeleteConsumableVolumeExpertCommand> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeExpert?>> Handle(DeleteConsumableVolumeExpertCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumableVolumeExpert>(ConsumableVolumeExpertErrors.ProjectOperationDetailWithIdNotFound);
            if (entity.IsStandard)
                return Result.Failure<ConsumableVolumeExpert>(ConsumableVolumeExpertErrors.DataIsStandard);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeExpert>(SharedErrors.UnknownError);
        }
    }
}
