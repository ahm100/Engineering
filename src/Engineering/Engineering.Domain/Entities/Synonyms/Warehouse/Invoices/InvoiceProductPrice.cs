using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

[NotMapped]
public class ViewInvoiceProductPrice : ActivateEntity<ViewInvoiceProductPrice>
{
    public long InvoiceProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? ShippingPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public decimal? PackagingPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal? TaxPrice { get; set; }
    public long CurrencyId { get; set; }
    public decimal CurrencyRate { get; set; }
    public PricingMethodType PricingMethodType { get; set; }
    public decimal? AvragePrice { get; set; }
    public decimal PricedQuantity { get; set; }
    public long? PricedInvoiceProductId { get; set; }
    public ViewInvoiceProduct? InvoiceProduct { get; set; }
    public ViewInvoiceProduct? PricedInvoiceProduct { get; set; }
    public DocType DocType { get; set; }
    public long? AccountingDocumentId { get; set; }
    public long? AccountingDocumentNumber { get; set; }

}
public enum DocType
{
    [Description("وارده")]
    Input = 0,

    [Description("صادره")]
    Output = 1,
}
public enum PricingMethodType
{
    [Description("خرید")] PreInvoice = 0,
    [Description("آخرین خرید در انبار")] LastInWarehose = 1,
    [Description("آخرین خرید در موسسه")] LastInCompany = 2,
    [Description("دستی")] Manual = 3,


    [Description("میانگین موزون")] WeightedAverage = 10,
    [Description("FIFO")] Fifo = 12,
    [Description("LIFO")] Lifo = 13,
    [Description("شناسایی ویژه")] SpecialIdentification = 14,
}
