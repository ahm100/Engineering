namespace Engineering.Application.Services.OperationInfos.Models.GetsByMultiFilter;

public record GetsByMultiFilterRequest(
    string FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
