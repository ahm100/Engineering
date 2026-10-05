using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class RequestGoodsSupplyTypeDetailRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyTypeDetail>, IRequestGoodsSupplyTypeDetailRepository
{
    public RequestGoodsSupplyTypeDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetDetailByRGSIdModel>? Data, int RowCount)> GetDetailByRGSId(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.RequestGoodsSupplyType.RequestGoodsSupplyId == id)
            .Select(x => new GetDetailByRGSIdModel
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                CreatorId = x.CreatorId,
                ReferenceId = x.ReferenceId,
                Type = x.Type,
                CostCenterName = x.CostCenterId != null ? x.CostCenter.CostCenterName : null,
                CostCenterEnName = x.CostCenterId != null ? x.CostCenter.CostCenterEnName : null,
                DeliveryDeadLine = x.DelivaryDeadLine,
                RequestGoodsSupplyTypeId = x.RequestGoodsSupplyTypeId.Value,
                RequestedCount = x.RequestedCount,
                Urls = x.RequestGoodsSupplyTypeDetailDocuments.Select(x => x.Url).ToList()
            });

        var count = await query.CountAsync();

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }

    public async Task<(List<GetDetailByRGSTypeIdModel>? Data, int RowCount)> GetDetailByRGSTypeId(
    long id,
    List<RGSTypeStatus>? statuses,
    int pageIndex,
    int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.RequestGoodsSupplyTypeId == id &&
            (statuses == null || statuses.Contains(x.RequestGoodsSupplyType.Status)))
            .Select(x => new GetDetailByRGSTypeIdModel
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                CostCenterName = x.CostCenterId != null ? x.CostCenter.CostCenterName : null,
                DeliveryDeadLine = x.DelivaryDeadLine,
                RequestedCount = x.RequestedCount,
                Description = x.Description,
                DescriptionEn = x.DescriptionEn,
                Urls = x.RequestGoodsSupplyTypeDetailDocuments.Select(x => x.Url).ToList()
            });

        var count = await query.CountAsync();

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}