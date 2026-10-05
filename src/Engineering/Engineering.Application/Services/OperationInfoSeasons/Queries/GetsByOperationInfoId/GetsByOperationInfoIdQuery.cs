using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsByOperationInfoId;

public record GetsByOperationInfoIdQuery(
    long OprationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoSeason>>>;
