using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.UpdateExpertConsumableVolume;

public class UpdateConsumableVolumeExpertCommandHandler : ICommandHandler<UpdateConsumableVolumeExpertCommand, ConsumableVolumeExpert>
{
    private readonly ILogger<UpdateConsumableVolumeExpertCommand> _logger;
    private readonly IConsumableVolumeExpertRepository _repository;

    public UpdateConsumableVolumeExpertCommandHandler(ILogger<UpdateConsumableVolumeExpertCommand> logger,
        IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeExpert?>> Handle(UpdateConsumableVolumeExpertCommand request, CT ct)
    {
        try
        {
            var entity = request.ConsumableVolumeExpert;
            entity.SetExpertId(request.ExpertId);
            entity.SetFinalValue(request.FinalValue);
            entity.SetNumber(request.Number ?? 0);
            entity.SetUnusedPercentage(request.UnusedPercentage);
            entity.SetIsStandard(request.IsStandard);
            entity.SetStandardValue(request.StandardValue);

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