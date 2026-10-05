using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineryGetsByOperationInfoId;

public class MachineryGetsByOperationInfoIdQueryHandler : IQueryHandler<MachineryGetsByOperationInfoIdQuery, DataResult<List<ConsumptionStandardMachinery>>>
{
    private readonly IConsumptionStandardMachineryRepository _repository;
    private readonly ILogger<MachineryGetsByOperationInfoIdQueryHandler> _logger;

    public MachineryGetsByOperationInfoIdQueryHandler(ILogger<MachineryGetsByOperationInfoIdQueryHandler> logger, IConsumptionStandardMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumptionStandardMachinery>>?>> Handle(MachineryGetsByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.MachineryGetByOprationInfoId(request.OprationInfoId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumptionStandardMachinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumptionStandardMachinery>>>(MachineryStandardErrors.MachineriesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumptionStandardMachinery>>>(SharedErrors.UnknownError);
        }
    }
}