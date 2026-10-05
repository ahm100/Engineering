
namespace Engineering.Application.Services.ProjectOperationDetails.Models.SetProjectOperationDetailPriority;

public record SetProjectOperationDetailPriorityRequest(
    long Id,
    int Priority
     ) : IHttpRequest;
