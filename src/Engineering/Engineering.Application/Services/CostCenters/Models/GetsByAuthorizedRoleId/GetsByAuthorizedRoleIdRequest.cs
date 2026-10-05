namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;

public record GetsByAuthorizedRoleIdRequest(
    string? FilterData,
    long RoleId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
