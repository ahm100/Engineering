
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailCodeCreator;

public record ProjectOperationDetailCodeCreatorRequest(
    long? ProjectOperationId,
    long? OperationLocationId
    ) : IHttpRequest;
