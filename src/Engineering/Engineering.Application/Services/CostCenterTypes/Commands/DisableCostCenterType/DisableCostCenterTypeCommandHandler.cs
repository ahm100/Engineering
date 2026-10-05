using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.DisableCostCenterType;

public class DisableCostCenterTypeCommandHandler : ICommandHandler<DisableCostCenterTypeCommand, CostCenterType>
{
    private readonly ILogger<DisableCostCenterTypeCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public DisableCostCenterTypeCommandHandler(
        ILogger<DisableCostCenterTypeCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(DisableCostCenterTypeCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<CostCenterType>(CostCenterErrors.CostCenterWithIdNotFound);
            if (entity.CostCenters.Count > 0)
                return Result.Failure<CostCenterType>(CostCenterErrors.CanNotDeleteBecuseOfCostCenter);

            entity.SoftDelete();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}