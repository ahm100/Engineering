namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;

public record GetPOWbsByProjectWbsIdRequest(
    long ProjectWbsId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;