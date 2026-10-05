using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Queries.GetsByOperationInfoId;

public record GetsByOperationInfoIdQuery(
    long OprationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoGroupRelation>>>;
