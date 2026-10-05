using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewInvoiceRepository : BaseRepository<EngineeringDBContext, ViewInvoice>, IViewInvoiceRepository
{
    public ViewInvoiceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewInvoice?> GetInvoiceByRequestNumber(
        long requestNumber,
        CT ct)
    {
        var query = await DbSet
            .FirstOrDefaultAsync(x => !x.IsDeleted &&
            x.PackingNumber == requestNumber &&
            (x.OwnerId != null || !string.IsNullOrEmpty(x.OwnerName)), ct);

        return query;
    }

    public async Task<List<ViewInvoice>?> GetInvoiceByPackingRequestNumbers(
        List<long> requestNumbers,
        CT ct)
    {
        var query = await DbSet
            .Where(x => !x.IsDeleted &&
                (x.PackingNumber != null && requestNumbers.Contains(x.PackingNumber.Value)) &&
                (x.OwnerId != null || !string.IsNullOrEmpty(x.OwnerName))).ToListAsync(ct);

        return query;
    }

    public async Task<List<ViewInvoice>?> GetInvoiceByRequestNumbers(
        List<long> requestNumbers,
        CT ct)
    {
        var invoices = DbContext.Set<ViewInvoice>().AsQueryable();
        var query = await invoices
        .Where(x => !x.IsDeleted &&
        requestNumbers.Contains(x.PackingNumber.Value) &&
        (x.OwnerId != null || !string.IsNullOrEmpty(x.OwnerName)))
        .ToListAsync(ct);

        return query;
    }

    public async Task<List<ViewInvoice>?> GetInvoiceByParentIds(
        List<long> parentIds,
        CT ct)
    {
        var invoices = DbContext.Set<ViewInvoice>().AsQueryable();
        var query = await invoices
        .Where(x => !x.IsDeleted &&
        (x.ParentId != null && parentIds.Contains(x.ParentId.Value)))
        .ToListAsync(ct);

        return query;
    }

    public async Task<ViewInvoice?> GetInvoiceByPackingNumber(long number, CT ct)
    {
        var query = await DbSet
            .Include(x => x.InvoiceProducts.Where(y => y.IsActive && !y.IsDeleted))
                .ThenInclude(x => x.InvoiceProductPrices)
            .Include(x => x.Warehouse)
            .Include(x => x.DestinationWarehouse)

            .FirstOrDefaultAsync(x => !x.IsDeleted && x.PackingNumber == number, ct);

        return query;
    }

    public async Task<List<ViewInvoice>> GetInvoicesByIds(
        List<long> ids,
        CT ct)
    {
        return await DbSet
            .Where(x => !x.IsDeleted && ids.Contains(x.Id))
            .ToListAsync(ct);
    }
}
