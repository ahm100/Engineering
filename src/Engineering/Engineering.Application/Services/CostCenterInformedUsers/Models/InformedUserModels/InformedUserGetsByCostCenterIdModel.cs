namespace Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserModels;

public record InformedUserGetsByCostCenterIdModel(
    long Id,
    string? FullName,
    string? OrganizationCode
    );
