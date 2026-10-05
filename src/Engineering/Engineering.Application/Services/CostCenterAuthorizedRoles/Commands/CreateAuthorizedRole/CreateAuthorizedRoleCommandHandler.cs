using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRole;

public class CreateAuthorizedRoleCommandHandler : ICommandHandler<CreateAuthorizedRoleCommand, CostCenterAuthorizedRole>
{
    private readonly ILogger<CreateAuthorizedRoleCommand> _logger;
    private readonly ICostCenterAuthorizedRoleRepository _repository;

    public CreateAuthorizedRoleCommandHandler(ILogger<CreateAuthorizedRoleCommand> logger, ICostCenterAuthorizedRoleRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterAuthorizedRole?>> Handle(CreateAuthorizedRoleCommand request, CT ct)
    {
        try
        {
            return await _repository.Create(new CostCenterAuthorizedRole(request.CostCenter, request.AuthorizedRoleId), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterAuthorizedRole>(SharedErrors.UnknownError);
        }
    }
}