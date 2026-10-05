using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationId;

public class GetMachineriesByProjectOperationIdQueryHandler : IQueryHandler<GetMachineriesByProjectOperationIdQuery, DataResult<List<MachineriesDataModel>>>
{
    private readonly IConsumableVolumeMachineryRepository _repository;
    private readonly ILogger<GetMachineriesByProjectOperationIdQueryHandler> _logger;

    public GetMachineriesByProjectOperationIdQueryHandler(ILogger<GetMachineriesByProjectOperationIdQueryHandler> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineriesDataModel>>?>> Handle(GetMachineriesByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationId(request.ProjectOperationId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineriesDataModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineriesDataModel>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineriesDataModel>>>(SharedErrors.UnknownError);
        }
    }
}