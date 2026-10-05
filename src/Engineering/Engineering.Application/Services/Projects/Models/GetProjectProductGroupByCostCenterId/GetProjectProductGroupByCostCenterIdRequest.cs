namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;

public record GetProjectProductGroupByCostCenterIdRequest(
    long ProjectId,
    long? CostCenterId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
