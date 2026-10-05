using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewInvoiceProductRepository : IBaseRepository<ViewInvoiceProduct>
{
    Task<ViewInvoiceProduct?> GetInvoiceProductById(
            long id,
            CT ct);
    Task<InvoiceProductWithDetailsDto?> GetInvoiceProductByPackingProductId(
            long id,
            CT ct);

    Task<(List<ViewInvoiceProduct> Data, int RowCount)> GetInvoiceProducts(
            long invoiceId,
            string? name,
            string? code,
            string? filterData,
            long? brandId,
            long? brandModelId,
            int pageIndex,
            int pageSize,
            CT ct);

    Task<List<ViewInvoiceProduct>> GetInvoiceProductByIds(
            List<long> ids,
            CT ct);
}