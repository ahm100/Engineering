namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetailComment;

public record CreateProjectOperationDetailCommentRequest(long ProjectOperationDetailId,
                                                         string Comment,
                                                         long? Parent,
                                                         List<string>? Documents) : IHttpRequest;
