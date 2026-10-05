namespace Engineering.Application.Services.ProjectOperations.Models.GetsByOperationInfo;

public record GetsByOperationInfoRequest(
    long OperationInfoId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
