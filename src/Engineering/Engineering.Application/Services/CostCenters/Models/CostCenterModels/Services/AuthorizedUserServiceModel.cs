namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.Services;

public record AuthorizedUserServiceModel(
    long AuthorizedUserId,
    string? Name,
    string? Code
    );
