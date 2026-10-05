using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceByIdNoInclude;

public record GetProjectServiceByIdNoIncludeQuery(
    long Id
    ) : IQuery<GetProjectServiceByIdResponse>;