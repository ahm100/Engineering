
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToTemporaryDeliveryRequest(
    long Id,
    string? StatusDescription
     ) : IHttpRequest;
