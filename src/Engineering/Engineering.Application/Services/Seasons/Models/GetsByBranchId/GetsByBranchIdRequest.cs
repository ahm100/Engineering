namespace Engineering.Application.Services.Seasons.Models.GetsByBranchId;

public record GetsByBranchIdRequest(
    long BranchId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
