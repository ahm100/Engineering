using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoSeasonRepository : IBaseRepository<OperationInfoSeason>
{
    Task<(List<OperationInfoSeason> Data, int RowCount)> GetsByOprationInfoId(long oprationInfoId, int pageIndex, int pageSize, CT ct);
    Task<(List<OperationInfoSeason> Data, int RowCount)> GetByOprationInfoIds(List<long>? oprationInfoIds, int pageIndex, int pageSize, CT ct);
    Task<(List<OperationInfoSeason> Data, int RowCount)> GetsOperationInfoSeasonByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct);
    Task<OperationInfoSeason?> GetById(long seasonId,
        long oprationInfoId, CT ct);
    Task<List<OperationInfoSeason>?> GetBySeasonAndOI(
        long seasonId,
        long oprationInfoId, CT ct);
    Task<List<OperationInfoSeason>?> GetBySeasonIdsAndOIIds(
        List<long> seasonIds,
        List<long> oprationInfoIds, CT ct);
    Task<OperationInfoSeason?> GetOperationInfoSeasonById(long Id, CT ct);
    Task<OperationInfoSeason?> GetOperationInfoSeasonByIdForDelete(long Id, CT ct);
    Task<(List<OperationInfoSeason> Data, int RowCount)> GetsOperationInfoSeasonFiltered(long? categoryId, long? branchId, long? seasonId, string[]? orderBy, int pageIndex, int pageSize, CT ct);

}