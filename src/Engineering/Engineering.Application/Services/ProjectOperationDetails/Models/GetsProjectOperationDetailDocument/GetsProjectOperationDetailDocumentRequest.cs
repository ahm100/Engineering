
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;

public record GetsProjectOperationDetailDocumentRequest(
    long ProjectOperationDetailId
    ) : IHttpRequest;
