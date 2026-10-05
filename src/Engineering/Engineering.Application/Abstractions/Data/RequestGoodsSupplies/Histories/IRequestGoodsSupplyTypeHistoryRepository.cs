using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Histories;

public interface IRequestGoodsSupplyTypeHistoryRepository : IBaseRepository<RequestGoodsSupplyTypeHistory>
{
    Task<(List<GetReferenceTypeHistoryModel>? Data, int RowCount)> GetReferenceTypeHistory(
        long referenceId,
        SupplyType supplyType,
        int pageIndex,
        int pageSize, CT ct);
}