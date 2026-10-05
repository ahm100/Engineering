using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewInvoiceRepository : IBaseRepository<ViewInvoice>
{
    Task<ViewInvoice?> GetInvoiceByRequestNumber(
            long requestNumber,
            CT ct);

    Task<List<ViewInvoice>?> GetInvoiceByPackingRequestNumbers(
        List<long> requestNumbers,
        CT ct);

    Task<List<ViewInvoice>?> GetInvoiceByRequestNumbers(
        List<long> requestNumbers,
        CT ct);

    Task<ViewInvoice?> GetInvoiceByPackingNumber(
            long number,
            CT ct);

    Task<List<ViewInvoice>> GetInvoicesByIds(
            List<long> ids,
            CT ct);

    Task<List<ViewInvoice>?> GetInvoiceByParentIds(
        List<long> parentIds,
        CT ct);
}