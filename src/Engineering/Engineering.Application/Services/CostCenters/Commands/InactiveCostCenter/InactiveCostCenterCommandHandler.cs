using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.InactiveCostCenter;

public class InactiveCostCenterCommandHandler : ICommandHandler<InactiveCostCenterCommand, CostCenter>
{
    private readonly ILogger<InactiveCostCenterCommand> _logger;
    private readonly ICostCenterRepository _repository;

    public InactiveCostCenterCommandHandler(ILogger<InactiveCostCenterCommand> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(InactiveCostCenterCommand request, CT ct)
    {
        try
        {
            var costCenterEntity = await _repository.FindByIdAndChild(request.Id, ct);
            if (costCenterEntity is null)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
            }
            if (costCenterEntity.IsActive == false)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.IsInactive);
            }
            if (costCenterEntity.IsDeleted == true)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.IsDeleted);
            }

            costCenterEntity.SetDeactivate();
            costCenterEntity.AddHistory();
            await _repository.Update(costCenterEntity);

            return costCenterEntity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}