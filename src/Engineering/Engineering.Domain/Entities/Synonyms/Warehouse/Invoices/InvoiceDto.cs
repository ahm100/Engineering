namespace Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;


public class InvoiceProductWithDetailsDto
{
    public ViewInvoiceProduct? InvoiceProduct { get; set; }
    public ViewInvoice? Invoice { get; set; }
    public List<ViewInvoiceProductPrice>? ProductPrices { get; set; }
}
