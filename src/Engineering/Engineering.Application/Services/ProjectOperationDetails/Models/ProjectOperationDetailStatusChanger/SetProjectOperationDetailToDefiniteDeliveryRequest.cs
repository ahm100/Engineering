
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToDefiniteDeliveryRequest(
    long Id,
    string? StatusDescription
     ) : IHttpRequest;
