namespace Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserGetsByCostCenterId;

public record InformedUserGetsByCostCenterIdRequest(
    long CostCenterId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
