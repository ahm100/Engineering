using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetCriticalPO;

public record GetCriticalPOQuery(
    long ProjectId,
    int PageIndex,
    int PageSize) : IQuery<GetCriticalPOResponse?>;