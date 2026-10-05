
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsBySeasonId;

public record GetsBySeasonIdQuery(
    long SeasonId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;
