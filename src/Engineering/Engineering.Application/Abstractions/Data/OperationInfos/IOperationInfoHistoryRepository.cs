using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;
using Engineering.Domain.Entities.OperationInfoHistorys;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoHistoryRepository : IBaseRepository<OperationInfoHistory>
{
    Task<(List<GetsOperationInfoHistoryByIdModel> Data, int RowCount)> GetsOperationInfoHistoryById(
        long operationInfoId,
        int pageIndex,
        int pageSize,
        CT ct);
}