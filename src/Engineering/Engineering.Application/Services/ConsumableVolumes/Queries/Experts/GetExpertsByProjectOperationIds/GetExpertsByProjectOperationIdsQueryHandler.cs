using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationIds;

public class GetExpertsByProjectOperationIdsQueryHandler : IQueryHandler<GetExpertsByProjectOperationIdsQuery, DataResult<List<ConsumableVolumeExpert>>>
{
    private readonly IConsumableVolumeExpertRepository _repository;
    private readonly ILogger<GetExpertsByProjectOperationIdsQueryHandler> _logger;

    public GetExpertsByProjectOperationIdsQueryHandler(ILogger<GetExpertsByProjectOperationIdsQueryHandler> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeExpert>>?>> Handle(GetExpertsByProjectOperationIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetExpertsByProjectOperationIds(request.ProjectOperationIds, ct);

            return result.Any() ?
                new DataResult<List<ConsumableVolumeExpert>>
                {
                    Data = result,
                } : Result.Failure<DataResult<List<ConsumableVolumeExpert>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeExpert>>>(SharedErrors.UnknownError);
        }
    }
}
