namespace Engineering.Application.Services.ProjectOperations.Models.GetsPrioritizeProjectOperation;

public record GetsPrioritizeProjectOperationRequest(
    string? FilterData,
    long? Id,
    int Priority,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
