using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.ActiveCostCenterType;

public class ActiveCostCenterTypeCommandHandler : ICommandHandler<ActiveCostCenterTypeCommand, CostCenterType>
{
    private readonly ILogger<ActiveCostCenterTypeCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public ActiveCostCenterTypeCommandHandler(
        ILogger<ActiveCostCenterTypeCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(ActiveCostCenterTypeCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<CostCenterType>(CostCenterTypeErrors.CostCenterTypeWithIdNotFound);
            if (entity.IsActive)
                return Result.Failure<CostCenterType>(CostCenterTypeErrors.IsActive);

            entity.SetActive();
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