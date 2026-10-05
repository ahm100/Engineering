
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

/// <summary>
/// شرح عملیات پروژه صورت وضعیت پیمانکار
/// </summary>
public class ContractorStatusStatementProduct : AuditableEntity<ContractorStatusStatementProduct>
{
    [Description(RGSCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; } = 0;

    [Description(RGSCmts.TotalSupplyCount)]
    public decimal TotalSupplyCount { get; private set; }

    [Description(RGSCmts.RegistrationDate)]
    public DateTime RegistrationDate { get; private set; }

    [Description(RGSCmts.Price)]
    public decimal? Price { get; private set; }

    [Description(RGSCmts.OtherPrice)]
    public decimal? OtherPrice { get; private set; }

    [Description(RGSCmts.TransferPrice)]
    public decimal? TransferPrice { get; private set; }

    [Description(RGSCmts.TaxNumber)]
    public decimal? TaxNumber { get; private set; }

    [Description(RGSCmts.DiscountOnInvoiceNumber)]
    public decimal? DiscountOnInvoiceNumber { get; private set; }

    [Description(RGSCmts.CustomerInvoiceNumber)]
    public string? CustomerInvoiceNumber { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.ContractorStatusStatement)]
    public long ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyDetail)]
    public long RequestGoodsSupplyDetailId { get; private set; }
    public RequestGoodsSupplyDetail RequestGoodsSupplyDetail { get; private set; }

    [Description(RGSCmts.IsPurchaseForContractor)]
    public bool IsPurchaseForContractor { get; private set; } = false;

    public ContractorStatusStatementProduct(
        ContractorStatusStatement contractorStatusStatement,
        RequestGoodsSupplyDetail requestGoodsSupplyDetail,
        long productId,
        decimal requestedCount,
        decimal totalSupplyCount,
        DateTime registrationDate,
        bool isPurchaseForContractor,
        decimal? price,
        decimal? otherPrice,
        decimal? transferPrice,
        decimal? discountOnInvoiceNumber,
        decimal? taxNumber,
        decimal? totalPrice,
        string? customerInvoiceNumber
        ) : this()
    {
        SetContractorStatusStatement(contractorStatusStatement);
        SetRequestGoodsSupplyDetail(requestGoodsSupplyDetail);
        SetIsPurchaseForContractor(isPurchaseForContractor);
        SetProductId(productId);
        SetRequestedCount(requestedCount);
        SetTotalSupplyCount(totalSupplyCount);
        SetRegistrationDate(registrationDate);
        SetPrice(price);
        SetOtherPrice(otherPrice);
        SetTransferPrice(transferPrice);
        SetDiscountOnInvoiceNumber(discountOnInvoiceNumber);
        SetTaxNumber(taxNumber);
        SetTotalPrice(totalPrice);
        SetCustomerInvoiceNumber(customerInvoiceNumber);
    }

    public void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestGoodsSupplyDetail(RequestGoodsSupplyDetail value)
    {
        RequestGoodsSupplyDetail = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProductId(long value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsPurchaseForContractor(bool value)
    {
        IsPurchaseForContractor = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalSupplyCount(decimal value)
    {
        TotalSupplyCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetRegistrationDate(DateTime value)
    {
        RegistrationDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetPrice(decimal? value)
    {
        Price = value;
    }

    public void SetOtherPrice(decimal? value)
    {
        OtherPrice = value;
    }

    public void SetTransferPrice(decimal? value)
    {
        TransferPrice = value;
    }

    public void SetDiscountOnInvoiceNumber(decimal? value)
    {
        DiscountOnInvoiceNumber = value;
    }

    public void SetTaxNumber(decimal? value)
    {
        TaxNumber = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
    }

    public void SetCustomerInvoiceNumber(string? value)
    {
        CustomerInvoiceNumber = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementProduct()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
