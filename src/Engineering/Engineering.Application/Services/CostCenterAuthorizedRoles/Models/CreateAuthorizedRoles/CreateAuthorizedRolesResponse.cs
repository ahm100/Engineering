namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;

public record CreateAuthorizedRolesResponse(
    long CostCenterId,
    bool IsCreated
    );
