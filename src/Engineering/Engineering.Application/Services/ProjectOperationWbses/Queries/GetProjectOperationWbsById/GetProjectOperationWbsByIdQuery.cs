using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetProjectOperationWbsById;

public record GetProjectOperationWbsByIdQuery(
    long Id) : IQuery<GetProjectOperationWbsByIdResponse?>;