namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;

public record GetsByAuthorizedUserIdRequest(
    string? FilterData,
    long UserId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
