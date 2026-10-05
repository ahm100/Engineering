using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroupAdmin;

public class DeleteCostCenterVirtualGroupAdminCommandHandler : ICommandHandler<DeleteCostCenterVirtualGroupAdminCommand, CostCenterVirtualGroupAdmin>
{
    private readonly ILogger<DeleteCostCenterVirtualGroupAdminCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupAdminRepository _repository;

    public DeleteCostCenterVirtualGroupAdminCommandHandler(ILogger<DeleteCostCenterVirtualGroupAdminCommandHandler> logger,
                                                           ICostCenterVirtualGroupAdminRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroupAdmin?>> Handle(DeleteCostCenterVirtualGroupAdminCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.CostCenterVirtualGroupAdminId, ct);

            if (entity is null)
                return Result.Failure<CostCenterVirtualGroupAdmin>(CostCenterVirtualGroupAdminErrors.CostCenterVirtualGroupAdminNotFound);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterVirtualGroupAdmin>(CostCenterVirtualGroupAdminErrors.IsDeleted);

            entity.SoftDelete();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroupAdmin>(SharedErrors.UnknownError);
        }
    }
}

