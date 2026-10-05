using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetsByProjectOperationDetailId;

public class GetExpertsByProjectOperationDetailIdQueryHandler : IQueryHandler<GetExpertsByProjectOperationDetailIdQuery, DataResult<List<ConsumableVolumeExpert>>>
{
    private readonly IConsumableVolumeExpertRepository _repository;
    private readonly ILogger<GetExpertsByProjectOperationDetailIdQueryHandler> _logger;

    public GetExpertsByProjectOperationDetailIdQueryHandler(ILogger<GetExpertsByProjectOperationDetailIdQueryHandler> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeExpert>>?>> Handle(GetExpertsByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationDetailId(request.ProjectOperationDetailId, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumableVolumeExpert>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumableVolumeExpert>>>(ConsumableVolumeExpertErrors.ProjectOperationDetailExpertWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeExpert>>>(SharedErrors.UnknownError);
        }
    }
}