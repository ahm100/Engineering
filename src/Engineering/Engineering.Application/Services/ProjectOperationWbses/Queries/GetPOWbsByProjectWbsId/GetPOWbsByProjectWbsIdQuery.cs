using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByProjectWbsId;

public record GetPOWbsByProjectWbsIdQuery(
    long ProjectWbsId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetPOWbsByProjectWbsIdResponse?>;