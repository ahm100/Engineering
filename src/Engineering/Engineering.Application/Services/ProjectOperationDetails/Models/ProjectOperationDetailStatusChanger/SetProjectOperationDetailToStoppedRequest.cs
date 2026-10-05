
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToStoppedRequest(
    long Id,
    string? StatusDescription
     ) : IHttpRequest;
