namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailComment;

public record UpdateProjectOperationDetailCommentRequest(
    long Id,
    string Comment,
    List<UpdateProjectOperationDetailCommentDocumentModel>? Documents
    ) : IHttpRequest;
