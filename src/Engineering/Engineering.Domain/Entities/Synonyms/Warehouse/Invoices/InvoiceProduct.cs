using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

[NotMapped]
public class ViewInvoiceProduct : ActivateEntity<ViewInvoiceProduct>
{
    public long PackageId { get; set; }
    public long? PackingProductId { get; set; }
    public long InvoiceId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal ConfirmQuantity { get; set; }
    public decimal ConfirmTotalQuantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalPrice { get; set; }
    public long? CurrencyId { get; set; }
    public long? RefInvoiceProductId { get; set; }

    [ForeignKey("PackageId")]
    public ViewPackage? Package { get; set; }
    [ForeignKey("InvoiceId")]
    public virtual ViewInvoice? Invoice { get; set; }
    [ForeignKey("ProductId")]
    public ViewProduct? Product { get; set; }
    public InvoiceProductStatus Status { get; set; }
    public string? Description { get; set; }
    public string? ExtraDescription { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? ManufactureDate { get; set; }
    public Guid TrackingCode { get; set; }

    public virtual ICollection<ViewInvoiceProductPrice> InvoiceProductPrices { get; set; }

    public virtual ICollection<ViewInvoiceProductPrice> PricedInvoiceProductPrices { get; set; }

    public ViewInvoiceProduct()
    {
    }
}

public enum InvoiceProductStatus
{
    [Description("ثبت اولیه")]
    New = 0,

    [Description("تایید شده")]
    Approved = 1,

    [Description("رد شده")]
    Rejected = 2,

    [Description("رسیدگی در نوبت بعدی")]
    NextProcessing = 3,
}