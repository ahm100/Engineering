using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationProgress;

public record GetProjectOperationProgressQuery(
    long Id
     ) : IQuery<GetProjectOperationProgressResponse?>;