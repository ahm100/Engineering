using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoGroupRelationRepository : IBaseRepository<OperationInfoGroupRelation>
{
    Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsByOprationInfoId(long oprationInfoId, int pageIndex, int pageSize, CT ct);
    Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsByOprationInfoGroupId(long oprationInfoGroupId, int pageIndex, int pageSize, CT ct);
    Task<OperationInfoGroupRelation?> GetById(long groupId, long oprationInfoId, CT ct);
    Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsOperationInfoGroupRelationFiltered(long? oprationInfoId, long? oprationInfoGroupId, int pageIndex, int pageSize, CT ct);

}