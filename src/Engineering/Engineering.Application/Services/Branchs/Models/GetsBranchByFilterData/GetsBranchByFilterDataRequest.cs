namespace Engineering.Application.Services.Branchs.Models.GetsBranchByFilterData;

public record GetsBranchByFilterDataRequest(
    string? FilterData,
    string? BranchFilterData,
    int PageIndex,
    int PageSize)
    : IHttpRequest;