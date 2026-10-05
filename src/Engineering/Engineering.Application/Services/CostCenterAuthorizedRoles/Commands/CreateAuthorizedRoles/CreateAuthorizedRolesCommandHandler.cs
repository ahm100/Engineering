using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRoles;

public class CreateAuthorizedRolesCommandHandler : ICommandHandler<CreateAuthorizedRolesCommand, List<CostCenterAuthorizedRole?>>
{
    private readonly ILogger<CreateAuthorizedRolesCommand> _logger;
    private readonly ICostCenterAuthorizedRoleRepository _repository;

    public CreateAuthorizedRolesCommandHandler(ILogger<CreateAuthorizedRolesCommand> logger, ICostCenterAuthorizedRoleRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<Result<List<CostCenterAuthorizedRole?>>> Handle(CreateAuthorizedRolesCommand request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        try
        {
            var response = new List<CostCenterAuthorizedRole?>();
            if (request.CostCenter.CostCenterAuthorizedRoles is not null || request.CostCenter.CostCenterAuthorizedRoles?.Count > 0)
                foreach (var item in request.CostCenter.CostCenterAuthorizedRoles)
                    await _repository.Remove(item);

            foreach (var item in request.RoleIds)
                response.Add(await _repository.Create(new CostCenterAuthorizedRole(request.CostCenter, item), ct));

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<List<CostCenterAuthorizedRole?>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}