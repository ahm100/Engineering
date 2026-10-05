using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Histories;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeHistoryRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyTypeHistory>, IRequestGoodsSupplyTypeHistoryRepository
{
    public RequestGoodsSupplyTypeHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetReferenceTypeHistoryModel>? Data, int RowCount)> GetReferenceTypeHistory(
        long referenceId,
        SupplyType supplyType,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.ReferenceId == referenceId
            && x.Type == supplyType)
            .Select(x => new GetReferenceTypeHistoryModel
            {
                Id = x.Id,
                Importance = x.Importance,
                ReferenceId = x.ReferenceId,
                Type = x.Type,
                RequestedCount = x.RequestedCount,
                DelivaryDeadLine = x.DelivaryDeadLine,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice,
                PackingPrice = x.PackingPrice,
                FinalPrice = x.FinalPrice,
                Description = x.Description,
                ManagementDescription = x.ManagementDescription,
                ContractorId = x.ContractorId,
                PackageId = x.PackageId,
                PackageCount = x.PackageCount,
                PackageUnitPrice = x.PackageUnitPrice,
                ProjectName = x.ProjectName,
                ProjectCode = x.ProjectCode,
                ProjectEnName = x.ProjectEnName,
                LastDescription = x.LastDescription
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}