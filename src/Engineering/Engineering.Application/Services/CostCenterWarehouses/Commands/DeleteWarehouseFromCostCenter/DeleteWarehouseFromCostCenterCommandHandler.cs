using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.DeleteWarehouseFromCostCenter;

public class DeleteWarehouseFromCostCenterCommandHandler : ICommandHandler<DeleteWarehouseFromCostCenterCommand, CostCenterWarehouse>
{
    private readonly ILogger<DeleteWarehouseFromCostCenterCommand> _logger;
    private readonly ICostCenterWarehouseRepository _repository;

    public DeleteWarehouseFromCostCenterCommandHandler(ILogger<DeleteWarehouseFromCostCenterCommand> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(DeleteWarehouseFromCostCenterCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindCostCenterWarehouse(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.NotFoundIdForDelete);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.IsDeleted);

            entity.SoftDelete();
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