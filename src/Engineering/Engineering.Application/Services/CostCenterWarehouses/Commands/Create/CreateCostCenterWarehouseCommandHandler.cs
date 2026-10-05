using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Create;

public class CreateCostCenterWarehouseCommandHandler : ICommandHandler<CreateCostCenterWarehouseCommand, CostCenterWarehouse>
{
    private readonly ILogger<CreateCostCenterWarehouseCommand> _logger;
    private readonly ICostCenterWarehouseRepository _repository;

    public CreateCostCenterWarehouseCommandHandler(ILogger<CreateCostCenterWarehouseCommand> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(CreateCostCenterWarehouseCommand request, CT ct)
    {
        try
        {
            if (request.CostCenter.CostCenterWarehouses.Any(oo => oo.IsDefault) && request.IsDefault)
            {
                var defaultWarehouse = request.CostCenter.CostCenterWarehouses.Where(x => x.IsDefault).FirstOrDefault();
                if (defaultWarehouse is not null)
                {
                    defaultWarehouse.SetIsDefault(false);
                    await _repository.Update(defaultWarehouse);
                }
            }
            var entity = new CostCenterWarehouse(request.CostCenter, request.WarehouseId, request.IsDefault);

            return await _repository.Create(entity, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterWarehouse>(SharedErrors.UnknownError);
        }
    }
}