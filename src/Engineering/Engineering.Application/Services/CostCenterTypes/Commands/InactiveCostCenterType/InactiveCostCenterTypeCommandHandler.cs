using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.InactiveCostCenterType;

public class InactiveCostCenterTypeCommandHandler : ICommandHandler<InactiveCostCenterTypeCommand, CostCenterType>
{
    private readonly ILogger<InactiveCostCenterTypeCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public InactiveCostCenterTypeCommandHandler(
        ILogger<InactiveCostCenterTypeCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(InactiveCostCenterTypeCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<CostCenterType>(CostCenterTypeErrors.CostCenterTypeWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<CostCenterType>(CostCenterTypeErrors.IsInactive);

            entity.SetDeactivate();
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