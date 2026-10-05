namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;

public record DeleteAuthorizedRoleResponse(
    long Id,
    long CostCenterId,
    bool IsDeleted
    );
