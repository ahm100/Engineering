using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public partial class ViewPackingRepository : BaseRepository<EngineeringDBContext, ViewPacking>, IViewPackingRepository
{
    public ViewPackingRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewPacking?> GetPacking(
        long id,
        CT ct)
    {
        return await DbSet
            .Where(oo => oo.Id == id)

            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ViewPacking>> GetPackings(List<long> ids, CT ct)
    {
        var packings = await DbSet
            .Where(oo => ids.Contains(oo.Id))
            .ToListAsync(ct);

        return packings;
    }

    public async Task<ViewPacking?> GetPackingNumber(
        long requestNumber,
        CT ct)
    {
        return await DbSet
            .Where(oo => oo.RequestNumber == requestNumber)

            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> GetPackingByLegacyId(
        long legacyId, CT ct)
    {
        return await DbSet
            .Where(oo => oo.LegacyId == legacyId)
            .AnyAsync(ct);
    }

    public async Task<ViewPacking?> DeletePacking(
        long id, CT ct)
    {
        return await DbSet

            .Where(oo => oo.Id == id)

            .FirstOrDefaultAsync(ct);
    }

    public async Task<ViewPacking?> DeletePackingByCommercPackId(
        long commercPackId, CT ct)
    {
        return await DbSet

            .Where(oo => oo.CommercPackId == commercPackId)

            .FirstOrDefaultAsync(ct);
    }

    public async Task<ViewPacking?> GetByNumber(
        long number, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return await DbSet
            .Where(oo => oo.RequestNumber == number)
            .FirstOrDefaultAsync(ct);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public async Task<ViewPacking?> GetByNumberForAfterWarehouseConfirm(
        long number, CT ct)
    {
        return await DbSet
            .Where(p => p.RequestNumber == number)

            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ViewPacking>?> GetByNumbers(
        List<long> numbers, CT ct)
    {
        return await DbSet
                   .Where(oo => oo.RequestNumber.HasValue && numbers.Contains(oo.RequestNumber.Value))
                   .ToListAsync(ct);
    }

    public async Task<List<ViewPacking>?> GetByIdsWithInclude(
        List<long> ids, CT ct)
    {
        var packings = await DbSet
            .Where(oo => ids.Contains(oo.Id))
            .ToListAsync(ct);

        if (!packings.Any())
            return packings;

        var packingIds = packings.Select(p => p.Id).ToList();

        var packingShippings = await DbContext.Set<ViewPackingShippingDetail>().AsQueryable()
            .Where(pp => packingIds.Contains(pp.PackingId)).ToListAsync(ct);
        List<ViewPackingShippingDetail>? shippings = [];
        if (packingShippings != null && packingShippings.ToList().Count > 0)
            shippings = packingShippings;

        var packingPallets = await DbContext.Set<ViewPackingPallet>().AsQueryable()
            .Where(pp => packingIds.Contains(pp.PackingId)).ToListAsync(ct);
        List<ViewPackingPallet>? pallets = [];
        if (packingPallets != null && packingPallets.ToList().Count > 0)
            pallets = packingPallets;

        var packingAddresses = await DbContext.Set<ViewPackingAddress>().AsQueryable()
            .Where(pp => packingIds.Contains(pp.PackingId!.Value)).ToListAsync(ct);
        List<ViewPackingAddress>? addresses = [];
        if (packingAddresses != null && packingAddresses.ToList().Count > 0)
            addresses = packingAddresses;

        var packingProducts = await DbContext.Set<ViewPackingProduct>()
            .Where(pp => packingIds.Contains(pp.PackingId))
            .Include(pp => pp.PackingContainer)
            .Include(pp => pp.PackingPallet)
            .ThenInclude(ppp => ppp.PackagingSpec)
            .Include(pp => pp.SourcePackingAddress)
                .ThenInclude(a => a.Warehouse)
            .Include(pp => pp.DestinationPackingAddress)
                .ThenInclude(a => a.Warehouse)
            .ToListAsync(ct);

        foreach (var packing in packings)
        {
            packing.PackingProducts = packingProducts
                .Where(pp => pp.PackingId == packing.Id)
                .ToList();

            packing.PackingShippingDetails = shippings
                .Where(pp => pp.PackingId == packing.Id)
                .ToList();

            packing.PackingPallets = pallets
                .Where(pp => pp.PackingId == packing.Id)
                .ToList();

            packing.PackingAddress = addresses
                .Where(pp => pp.PackingId == packing.Id)
                .ToList();

            foreach (var item in packing.PackingProducts)
            {
                if (item.SourcePackingAddressId != null)
                {
                    item.SourcePackingAddress = await DbContext.Set<ViewPackingAddress>()
                        .Include(x => x.Warehouse)
                        .FirstOrDefaultAsync(x => x.Id == item.SourcePackingAddressId!.Value, ct);
                }

                var dest = await DbContext.Set<ViewPackingAddress>()
                    .Include(x => x.Warehouse)
                    .FirstOrDefaultAsync(x => x.Id == item.DestinationPackingAddressId, ct);
                if (dest is not null)
                {
                    item.DestinationPackingAddress = dest;
                }
            }
        }

        return packings;
    }

    public async Task<List<ViewPacking>?> GetByIds(
        List<long> ids, CT ct)
    {
        return await DbSet
               .Where(oo => ids.Contains(oo.Id))
                          .ToListAsync(ct);
    }
}
