
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailCommentById;

public record GetProjectOperationDetailCommentByIdRequest(long ProjectOperationDetailId,
                                                          long? ParentId = null) : IHttpRequest;
