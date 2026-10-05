using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.CreateExpertConsumableVolume;

public class CreateConsumableVolumeExpertCommandHandler : ICommandHandler<CreateConsumableVolumeExpertCommand, ConsumableVolumeExpert>
{
    private readonly ILogger<CreateConsumableVolumeExpertCommand> _logger;
    private readonly IConsumableVolumeExpertRepository _repository;

    public CreateConsumableVolumeExpertCommandHandler(ILogger<CreateConsumableVolumeExpertCommand> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeExpert?>> Handle(CreateConsumableVolumeExpertCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumableVolumeExpert(request.ProjectOperationDetail, request.ExpertId, request.Number ?? 0, request.UnusedPercentage,
                request.IsStandard, request.StandardValue, request.FinalValue);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeExpert>(SharedErrors.UnknownError);
        }
    }
}