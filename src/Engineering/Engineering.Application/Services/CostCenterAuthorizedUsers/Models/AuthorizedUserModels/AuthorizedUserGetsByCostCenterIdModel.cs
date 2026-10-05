namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserModels;

public record AuthorizedUserGetsByCostCenterIdModel(
    long Id,
    string? FullName,
    string? OrganizationCode,
    List<long>? RoleIds
    );
