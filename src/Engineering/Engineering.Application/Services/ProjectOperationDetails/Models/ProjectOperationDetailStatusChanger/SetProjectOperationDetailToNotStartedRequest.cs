
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToNotStartedRequest(
    long Id
     ) : IHttpRequest;
