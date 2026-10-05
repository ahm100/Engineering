namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;

public record DeleteAuthorizedUserResponse(
    long Id,
    long CostCenterId,
    bool IsDeleted
    );
