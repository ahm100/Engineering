using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyTypeDetailRepository : IBaseRepository<RequestGoodsSupplyTypeDetail>
{
    Task<(List<GetDetailByRGSIdModel>? Data, int RowCount)> GetDetailByRGSId(
    long id,
    int pageIndex,
    int pageSize, CT ct);

    Task<(List<GetDetailByRGSTypeIdModel>? Data, int RowCount)> GetDetailByRGSTypeId(
    long id,
    List<RGSTypeStatus>? statuses,
    int pageIndex,
    int pageSize, CT ct);
}