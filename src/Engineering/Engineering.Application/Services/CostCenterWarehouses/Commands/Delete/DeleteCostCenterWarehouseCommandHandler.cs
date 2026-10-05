using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Delete;

public class DeleteCostCenterWarehouseCommandHandler : ICommandHandler<DeleteCostCenterWarehouseCommand, CostCenterWarehouse>
{
    private readonly ILogger<DeleteCostCenterWarehouseCommand> _logger;
    private readonly ICostCenterWarehouseRepository _repository;

    public DeleteCostCenterWarehouseCommandHandler(ILogger<DeleteCostCenterWarehouseCommand> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(DeleteCostCenterWarehouseCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindCostCenterWarehouse(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.NotFoundIdForDelete);
            if (entity.IsDefault)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.IsDefaultWarehouse);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterWarehouse>(CostCenterWarehouseErrors.IsDeleted);

            await _repository.Remove(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterWarehouse>(SharedErrors.UnknownError);
        }
    }
}