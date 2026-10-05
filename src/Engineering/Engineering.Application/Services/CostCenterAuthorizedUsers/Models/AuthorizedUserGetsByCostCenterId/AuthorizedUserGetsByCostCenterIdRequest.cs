namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;

public record AuthorizedUserGetsByCostCenterIdRequest(
    long CostCenterId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
