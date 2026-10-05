using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetDetailPOWbsByProjectWbsId;

public record GetDetailPOWbsByProjectWbsIdQuery(
    long ProjectWbsId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetDetailPOWbsByProjectWbsIdResponse?>;