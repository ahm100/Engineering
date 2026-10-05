using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.ActiveCostCenter;

public class ActiveCostCenterCommandHandler : ICommandHandler<ActiveCostCenterCommand, CostCenter>
{
    private readonly ILogger<ActiveCostCenterCommand> _logger;
    private readonly ICostCenterRepository _repository;

    public ActiveCostCenterCommandHandler(ILogger<ActiveCostCenterCommand> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(ActiveCostCenterCommand request, CT ct)
    {
        try
        {
            var CostCenterEntity = await _repository.FindByIdAndChild(request.Id, ct);
            if (CostCenterEntity is null)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
            }
            if (CostCenterEntity.IsActive == true)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.IsActive);
            }
            if (CostCenterEntity.IsDeleted == true)
            {
                return Result.Failure<CostCenter>(CostCenterErrors.IsDeleted);
            }
            CostCenterEntity.SetActive();
            CostCenterEntity.AddHistory();
            await _repository.Update(CostCenterEntity);
            return CostCenterEntity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}