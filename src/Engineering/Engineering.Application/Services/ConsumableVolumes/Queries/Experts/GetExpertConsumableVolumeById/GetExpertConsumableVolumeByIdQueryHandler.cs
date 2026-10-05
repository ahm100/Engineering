using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;

public class GetConsumableVolumeExpertByIdQueryHandler : IQueryHandler<GetConsumableVolumeExpertByIdQuery, ConsumableVolumeExpert>
{
    private readonly ILogger<GetConsumableVolumeExpertByIdQueryHandler> _logger;
    private readonly IConsumableVolumeExpertRepository _repository;

    public GetConsumableVolumeExpertByIdQueryHandler(ILogger<GetConsumableVolumeExpertByIdQueryHandler> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeExpert?>> Handle(GetConsumableVolumeExpertByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            return result ?? Result.Failure<ConsumableVolumeExpert>(ConsumableVolumeExpertErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeExpert>(SharedErrors.UnknownError);
        }
    }
}
