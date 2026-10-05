
namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummarizedProjectOperation;

public record GetsSummarizedProjectOperationRequest(
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
