namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;

public record GetDetailPOWbsByProjectWbsIdRequest(
    long ProjectWbsId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;