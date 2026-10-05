using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;

public class GetConsumableVolumeMachineryByIdQueryHandler : IQueryHandler<GetConsumableVolumeMachineryByIdQuery, ConsumableVolumeMachinery>
{
    private readonly ILogger<GetConsumableVolumeMachineryByIdQueryHandler> _logger;
    private readonly IConsumableVolumeMachineryRepository _repository;

    public GetConsumableVolumeMachineryByIdQueryHandler(ILogger<GetConsumableVolumeMachineryByIdQueryHandler> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeMachinery?>> Handle(GetConsumableVolumeMachineryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            return result ?? Result.Failure<ConsumableVolumeMachinery>(ConsumableVolumeMachineryErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeMachinery>(SharedErrors.UnknownError);
        }
    }
}
