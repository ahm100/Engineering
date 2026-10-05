using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.ShippingCosts;

public class ShippingCostHistoryRepository : BaseRepository<EngineeringDBContext, ShippingCostHistory>, IShippingCostHistoryRepository
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public ShippingCostHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetShippingCostHistoryResponseModel> Data, int RowCount)> GetShippingCostHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => x.ShippingCost.Id == id)
            .Select(z => new GetShippingCostHistoryResponseModel
            {
                IsActive = z.IsActive,
                Count = z.Count,
                Created = z.Created,
                Id = z.Id,
                CreatorId = z.CreatorId,
                DestinationCityId = z.DestinationCityId,
                DestinationCity = z.DestinationCity.Name,
                FromDate = z.FromDate,
                Latitude = z.Latitude,
                Longitude = z.Longitude,
                LoadWeight = z.LoadWeight,
                MachinTypeId = z.MachineTypeId,
                MachinTypeCode = z.MachineType.MachineTypeCode,
                MachinTypeName = z.MachineType.MachineTypeTitle,
                Price = z.Price,
                Region = z.Region.Name,
                RegionId = z.RegionId,
                ShippingCostId = z.ShippingCostId,
                SourceCityId = z.SourceCityId,
                SourceCity = z.SourceCity.Name,
                ShippingThirdPartyId = z.ThirdPartyId,
                ToDate = z.ToDate,
                Tax = z.Tax,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
}