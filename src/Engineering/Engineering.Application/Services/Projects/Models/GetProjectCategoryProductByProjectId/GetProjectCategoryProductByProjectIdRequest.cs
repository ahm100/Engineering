namespace Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;

public record GetProjectCategoryProductByProjectIdRequest(
    long Id,
    long? CostCenterId
     ) : IHttpRequest;
