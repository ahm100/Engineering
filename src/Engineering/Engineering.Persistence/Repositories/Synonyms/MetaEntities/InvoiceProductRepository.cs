using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewInvoiceProductRepository : BaseRepository<EngineeringDBContext, ViewInvoiceProduct>, IViewInvoiceProductRepository
{
    public ViewInvoiceProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewInvoiceProduct?> GetInvoiceProductById(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(x => x.Invoice)

            .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id,
            ct);
    }

    public async Task<(List<ViewInvoiceProduct> Data, int RowCount)> GetInvoiceProducts(
        long invoiceId,
        string? name,
        string? code,
        string? filterData,
        long? brandId,
        long? brandModelId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Product)
            .Include(x => x.Invoice)
            .Include(e => e.Package)
            .Where(x =>
                !x.IsDeleted &&
                x.ConfirmQuantity > 0 &&
                x.InvoiceId == invoiceId &&
                (!brandModelId.HasValue || x.Product.BrandModelId == brandModelId) &&
                (string.IsNullOrWhiteSpace(name) || x.Product.Name.Contains(name)) &&
                (string.IsNullOrWhiteSpace(code) || x.Product.Code.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    x.Product.Name.Contains(filterData) ||
                    x.Quantity.ToString().Contains(filterData) ||
                    x.Product.Code.Contains(filterData)));

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ViewInvoiceProduct>> GetInvoiceProductByIds(
        List<long> ids,
        CT ct)
    {
        return await DbSet
            .Include(x => x.Invoice)
            .Where(x => ids.Contains(x.Id)).ToListAsync(ct);
    }

    public async Task<InvoiceProductWithDetailsDto?> GetInvoiceProductByPackingProductId(long id, CT ct)
    {
        var invoiceProduct = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(oo => oo.PackingProductId == id, ct);

        if (invoiceProduct is null)
            return null;

        var invoice = await DbContext.Set<ViewInvoice>()
            .AsNoTracking()
            .Where(pp => pp.Id == invoiceProduct.InvoiceId)
            .FirstOrDefaultAsync(ct);

        var productPrices = await DbContext.Set<ViewInvoiceProductPrice>()
            .AsNoTracking()
            .Where(pp => pp.InvoiceProductId == invoiceProduct.Id)
            .ToListAsync(ct);

        // استفاده از DTO به جای تنظیم navigation properties
        return new InvoiceProductWithDetailsDto
        {
            InvoiceProduct = invoiceProduct,
            Invoice = invoice,
            ProductPrices = productPrices
        };
    }
}
