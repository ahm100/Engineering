using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.Delete;

public class DeleteCostCenterCommandHandler : ICommandHandler<DeleteCostCenterCommand, CostCenter>
{
    private readonly ILogger<DeleteCostCenterCommand> _logger;
    private readonly ICostCenterRepository _repository;

    public DeleteCostCenterCommandHandler(ILogger<DeleteCostCenterCommand> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(DeleteCostCenterCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<CostCenter>(CostCenterErrors.IsDeleted);
            if (entity.Projects.Any())
                return Result.Failure<CostCenter>(CostCenterErrors.CanNotDeleteBecauseOfProject);

            entity.SoftDelete();
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}