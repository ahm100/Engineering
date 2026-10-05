using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Update;

public class UpdateCostCenterWarehouseCommandHandler : ICommandHandler<UpdateCostCenterWarehouseCommand, CostCenterWarehouse>
{
    private readonly ILogger<UpdateCostCenterWarehouseCommand> _logger;
    private readonly ICostCenterWarehouseRepository _repository;

    public UpdateCostCenterWarehouseCommandHandler(ILogger<UpdateCostCenterWarehouseCommand> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(UpdateCostCenterWarehouseCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForUpdate(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.NotFoundIdForUpdate);

            var defaultWarehouse = entity.CostCenter.CostCenterWarehouses.Where(x => x.IsDefault).FirstOrDefault();

            if ((defaultWarehouse is not null && defaultWarehouse.Id != entity.Id) && request.IsDefault == true)
                if (defaultWarehouse is not null)
                {
                    defaultWarehouse.SetIsDefault(false);
                    entity.SetIsDefault(request.IsDefault);
                    await _repository.Update(defaultWarehouse);
                }

            if (entity.CostCenter.CostCenterWarehouses.Count == 1)
            {
                if ((defaultWarehouse is not null && defaultWarehouse.Id == entity.Id) && request.IsDefault == false)
                    return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.CantChnageIsDefault);
            }
            else
            {
                entity.SetIsDefault(request.IsDefault);
            }

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