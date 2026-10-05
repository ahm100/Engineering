
namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummaryProjectOperation;

public record GetsSummaryProjectOperationRequest(
    long ProjectId,
    long? SeasonId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
