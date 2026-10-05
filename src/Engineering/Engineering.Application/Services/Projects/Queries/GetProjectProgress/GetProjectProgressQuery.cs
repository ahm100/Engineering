using Engineering.Application.Services.Projects.Models.GetProjectProgress;

namespace Engineering.Application.Services.Projects.Queries.GetProjectProgress;

public record GetProjectProgressQuery(
    long Id
     ) : IQuery<GetProjectProgressResponse?>;