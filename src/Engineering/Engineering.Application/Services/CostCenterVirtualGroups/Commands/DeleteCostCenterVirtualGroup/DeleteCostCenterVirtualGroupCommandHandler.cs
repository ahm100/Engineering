using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroup;

public class DeleteCostCenterVirtualGroupCommandHandler : ICommandHandler<DeleteCostCenterVirtualGroupCommand, CostCenterVirtualGroup>
{
    private readonly ILogger<DeleteCostCenterVirtualGroupCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupRepository _repository;

    public DeleteCostCenterVirtualGroupCommandHandler(ILogger<DeleteCostCenterVirtualGroupCommandHandler> logger,
                                                           ICostCenterVirtualGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroup?>> Handle(DeleteCostCenterVirtualGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.CostCenterVirtualGroupId, ct);

            if (entity is null)
                return Result.Failure<CostCenterVirtualGroup>(CostCenterVirtualGroupErrors.CostCenterVirtualGroupNotFound);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterVirtualGroup>(CostCenterVirtualGroupErrors.IsDeleted);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroup>(SharedErrors.UnknownError);
        }
    }
}
