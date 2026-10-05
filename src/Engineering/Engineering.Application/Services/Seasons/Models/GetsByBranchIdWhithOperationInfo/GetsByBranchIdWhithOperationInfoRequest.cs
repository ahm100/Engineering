namespace Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;

public record GetsByBranchIdWhithOperationInfoRequest(
    long BranchId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
