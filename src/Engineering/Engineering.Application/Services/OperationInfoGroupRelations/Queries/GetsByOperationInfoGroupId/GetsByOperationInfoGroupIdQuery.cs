using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Queries.GetsByOperationInfoGroupId;

public record GetsByOperationInfoGroupIdQuery(
    long OprationInfoGroupId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoGroupRelation>>>;
