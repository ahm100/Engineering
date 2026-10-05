using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailDocument;

public record GetsProjectOperationDetailDocumentQuery(
    long ProjectOperationDetailId
    ) : IQuery<GetsProjectOperationDetailDocumentResponse>;
