namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.Responses;

public record AuthorizedUserDataModel(
    long Id,
    string? FullName,
    string? OrganizationCode,
    List<long>? RoleIds
    );
