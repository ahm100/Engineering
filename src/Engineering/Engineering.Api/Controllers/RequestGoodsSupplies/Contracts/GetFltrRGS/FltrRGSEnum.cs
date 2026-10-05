using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetFltrRGS;

public enum FltrRGSEnum
{
    [Description(RGSCmts.RequestSerialNumber)]
    RequestSerialNumber = 1,

    [Description(RGSCmts.SerialNumber)]
    SerialNumber,

    [Description(GlobalCmts.Status)]
    StatusDescription,

    [Description(RGSCmts.Type)]
    TypeDescription,

    [Description(RGSCmts.PurchaseLocation)]
    PurchaseLocationDescription,

    [Description(RGSCmts.PurchaseReason)]
    PurchaseReasonDescription,

    [Description(RGSCmts.Supplier)]
    Supplyer,

    [Description(RGSCmts.Buyer)]
    Buyer,

    [Description(RGSCmts.Currency)]
    Currency,

    [Description(RGSCmts.TransferPrice)]
    TransferPrice,

    [Description(RGSCmts.OtherPrice)]
    OtherPrice,

    [Description(RGSCmts.DiscountOnInvoicePercentage)]
    DiscountOnInvoicePercentage,

    [Description(RGSCmts.DiscountOnInvoiceNumber)]
    DiscountOnInvoiceNumber,

    [Description(RGSCmts.DiscountedPriceOnInvoice)]
    DiscountedPriceOnInvoice,

    [Description(RGSCmts.TaxOnInvoicePercentage)]
    TaxOnInvoicePercentage,

    [Description(RGSCmts.TaxOnInvoiceNumber)]
    TaxOnInvoiceNumber,

    [Description(RGSCmts.FinalInvoiceAmount)]
    FinalInvoiceAmount,

    [Description(RGSCmts.RequestedDate)]
    RequestedDateShamsi,

    [Description(RGSCmts.DeliveryDeadLine)]
    DeliveryDeadlineShamsi,

    [Description(RGSCmts.RegistrationNumber)]
    RegistrationNumber,

    [Description(RGSCmts.RequestingOrganizationId)]
    RequestingOrganization,

    [Description(GlobalCmts.Description)]
    DescriptionEn,

    [Description(RGSCmts.IsPettyCash)]
    IsPettyCash,

    [Description(RGSCmts.ConsumptionAddress)]
    ConsumptionAddress,

    [Description(GlobalCmts.Project)]
    Project,

    [Description(GlobalCmts.Creator)]
    Creator
}