
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToEndOfWorkRequest(
    long Id,
    string? StatusDescription
     ) : IHttpRequest;
