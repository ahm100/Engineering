using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.DeleteAuthorizedRole;

public class DeleteAuthorizedRoleCommandHandler : ICommandHandler<DeleteAuthorizedRoleCommand, CostCenterAuthorizedRole>
{
    private readonly ILogger<DeleteAuthorizedRoleCommand> _logger;
    private readonly ICostCenterAuthorizedRoleRepository _repository;

    public DeleteAuthorizedRoleCommandHandler(ILogger<DeleteAuthorizedRoleCommand> logger, ICostCenterAuthorizedRoleRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterAuthorizedRole?>> Handle(DeleteAuthorizedRoleCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindRole(request.AuthorizedRoleId, request.CostCenterId, ct);
            if (entity is null)
                return Result.Failure<CostCenterAuthorizedRole>(CostCenterAuthorizedRoleErrors.WithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterAuthorizedRole>(CostCenterAuthorizedRoleErrors.IsDeleted);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterAuthorizedRole>(SharedErrors.UnknownError);
        }
    }
}