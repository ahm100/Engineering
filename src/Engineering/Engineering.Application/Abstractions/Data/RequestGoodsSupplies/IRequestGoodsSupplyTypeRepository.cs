using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyTypeRepository : IBaseRepository<RequestGoodsSupplyType>
{
    Task<RequestGoodsSupplyType?> GetById(long id,
        CT ct);

    Task<(List<GetRGSTypeByRGSIdModel>? Data, int RowCount)> GetRGSTypeByRGSId(
    long id,
    int pageIndex,
    int pageSize, CT ct);

    Task<(List<GetReferenceTypeHistoryModel>? Data, int RowCount)> GetReferenceTypeHistory(
    long referenceId,
    List<RGSTypeStatus>? statuses,
    SupplyType supplyType,
    int pageIndex,
    int pageSize, CT ct);
}