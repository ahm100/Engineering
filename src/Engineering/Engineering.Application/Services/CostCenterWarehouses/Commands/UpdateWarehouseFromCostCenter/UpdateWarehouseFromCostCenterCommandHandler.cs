using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.UpdateWarehouseFromCostCenter;

public class UpdateWarehouseFromCostCenterCommandHandler : ICommandHandler<UpdateWarehouseFromCostCenterCommand, CostCenterWarehouse>
{
    private readonly ILogger<UpdateWarehouseFromCostCenterCommand> _logger;
    private readonly ICostCenterWarehouseRepository _repository;

    public UpdateWarehouseFromCostCenterCommandHandler(ILogger<UpdateWarehouseFromCostCenterCommand> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(UpdateWarehouseFromCostCenterCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForUpdate(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.NotFoundIdForUpdate);

            entity.SetIsDefault(request.IsDefault);
            entity.SetWarehouseId(request.WarehouseId);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterWarehouse>(SharedErrors.UnknownError);
        }
    }
}