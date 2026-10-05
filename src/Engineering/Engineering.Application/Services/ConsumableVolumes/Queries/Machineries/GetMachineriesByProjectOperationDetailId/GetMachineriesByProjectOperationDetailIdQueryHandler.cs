using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationDetailId;

public class GetMachineriesByProjectOperationDetailIdQueryHandler : IQueryHandler<GetMachineriesByProjectOperationDetailIdQuery, DataResult<List<ConsumableVolumeMachinery>>>
{
    private readonly IConsumableVolumeMachineryRepository _repository;
    private readonly ILogger<GetMachineriesByProjectOperationDetailIdQueryHandler> _logger;

    public GetMachineriesByProjectOperationDetailIdQueryHandler(ILogger<GetMachineriesByProjectOperationDetailIdQueryHandler> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeMachinery>>?>> Handle(GetMachineriesByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationDetailId(request.ProjectOperationDetailId, request.FilterData, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumableVolumeMachinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumableVolumeMachinery>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeMachinery>>>(SharedErrors.UnknownError);
        }
    }
}