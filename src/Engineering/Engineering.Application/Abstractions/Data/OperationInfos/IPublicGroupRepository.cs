using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IPublicGroupRepository : IBaseRepository<PublicGroup>
{
    Task<PublicGroup?> GetById(long id, CT ct);
    Task<(List<PublicGroup> Data, int RowCount)> GetsPublicGroupFiltered(List<long>? productGroupIds, string[]? orderBy, int pageIndex, int pageSize, CT ct);
}