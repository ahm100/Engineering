namespace Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;

public record GetProjectProductCategoryByCostCenterIdRequest(
    long ProjectId,
    long? CostCenterId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;