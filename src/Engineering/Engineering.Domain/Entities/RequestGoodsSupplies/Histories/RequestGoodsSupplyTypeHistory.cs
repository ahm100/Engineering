using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeHistory : AuditableEntity<RequestGoodsSupplyTypeHistory>
{
    [Description(RGSCmts.Importance)]
    public GoodsSupplyDetailImportance Importance { get; private set; } = GoodsSupplyDetailImportance.Lowest;

    [Description(RGSCmts.Status)]
    public RGSTypeStatus Status { get; private set; } = RGSTypeStatus.Requested;

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DelivaryDeadLine { get; private set; }

    [Description(RGSCmts.ReferenceId)]
    public long? ReferenceId { get; private set; }

    [Description(RGSCmts.SupplyType)]
    public SupplyType Type { get; private set; }

    [Description(RGSCmts.PackageId)]
    public long? PackageId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RGSCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.TaxPercentage)]
    public decimal? TaxPercentage { get; private set; }

    [Description(RGSCmts.TaxNumber)]
    public decimal? TaxNumber { get; private set; }

    [Description(RGSCmts.DiscountByPercentage)]
    public decimal? DiscountByPercentage { get; private set; }

    [Description(RGSCmts.DiscountByNumber)]
    public decimal? DiscountByNumber { get; private set; }

    [Description(ProjectCmts.ProjectName)]
    public string? ProjectName { get; private set; }

    [Description(ProjectCmts.ProjectCode)]
    public string? ProjectCode { get; private set; }

    [Description(ProjectCmts.ProjectName)]
    public string? ProjectEnName { get; private set; }

    [Description(RGSCmts.DiscountedPrice)]
    public decimal? DiscountedPrice { get; private set; }

    [Description(RGSCmts.TransferPrice)]
    public decimal? TransferPrice { get; private set; }

    [Description(RGSCmts.FinalPrice)]
    public decimal? FinalPrice { get; private set; }

    [Description(RGSCmts.PackageCount)]
    public decimal? PackageCount { get; private set; }

    [Description(RGSCmts.PackageUnitPrice)]
    public decimal? PackageUnitPrice { get; private set; }

    [Description(RGSCmts.PackingPrice)]
    public decimal? PackingPrice { get; private set; }

    [Description(RGSCmts.CheckGroup)]
    public bool CheckGroup { get; private set; } = false;

    [Description(RGSCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(RGSCmts.DestinationWarehouseId)]
    public long? DestinationWarehouseId { get; private set; }

    [Description(RGSCmts.CustomerInvoiceNumber)]
    public string? CustomerInvoiceNumber { get; private set; }

    [Description(RGSCmts.RequestSerialNumber)]
    public string RequestSerialNumber => $"{SerialNumber}-{this.Id}";

    [Description(RGSCmts.SerialNumber)]
    public string SerialNumber { get; private set; }

    [Description(RGSCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.ManagementDescription)]
    public string? ManagementDescription { get; private set; }

    [Description(RGSCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyType)]
    public long RequestGoodsSupplyTypeId { get; private set; }
    public RequestGoodsSupplyType RequestGoodsSupplyType { get; private set; }

    public RequestGoodsSupplyTypeHistory(RequestGoodsSupplyType rgsType) : this()
    {
        SetRequestGoodsSupplyType(rgsType);

        SetStatus(rgsType.Status);

        SetImportance(rgsType.Importance);
        SetDelivaryDeadLine(rgsType.DelivaryDeadLine);
        SetReferenceId(rgsType.ReferenceId);
        SetSupplyType(rgsType.Type);
        SetPackageId(rgsType.PackageId);
        SetRequestedCount(rgsType.RequestedCount);
        SetUnitPrice(rgsType.UnitPrice);
        SetTotalPrice(rgsType.TotalPrice);
        SetPackingPrice(rgsType.PackingPrice);
        SetFinalPrice(rgsType.FinalPrice);
        SetPackageCount(rgsType.PackageCount);
        SetPackageUnitPrice(rgsType.PackageUnitPrice);
        SetCheckGroup(rgsType.CheckGroup);
        SetContractorId(rgsType.ContractorId);
        SetDescription(rgsType.Description);
        SetProjectName(rgsType.ProjectName);
        SetProjectEnName(rgsType.ProjectEnName);
        SetProjectCode(rgsType.ProjectCode);
        SetManagementDescription(rgsType.ManagementDescription);

        SerialNumber = rgsType.SerialNumber;
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetPackageId(long? value)
    {
        PackageId = value;
    }

    public void SetReferenceId(long? value)
    {
        ReferenceId = value;
    }

    public void SetProjectName(string? value)
    {
        ProjectName = value;
    }

    public void SetProjectEnName(string? value)
    {
        ProjectEnName = value;
    }

    public void SetProjectCode(string? value)
    {
        ProjectCode = value;
    }

    public void SetRequestGoodsSupplyType(RequestGoodsSupplyType value)
    {
        RequestGoodsSupplyType = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyTypeId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetSupplyType(SupplyType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetDelivaryDeadLine(DateTime? value)
    {
        DelivaryDeadLine = value;
    }

    public void SetDestinationWarehouseId(long? value)
    {
        DestinationWarehouseId = value;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = value;
    }

    public void SetUnitPrice(decimal? value)
    {
        UnitPrice = value;
    }

    public void SetTransferPrice(decimal? value)
    {
        TransferPrice = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
    }

    public void SetTaxPercentage(decimal? value)
    {
        TaxPercentage = value;
    }

    public void SetTaxNumber(decimal? value)
    {
        TaxNumber = value;
    }

    public void SetDiscountByPercentage(decimal? value)
    {
        DiscountByPercentage = value;
    }

    public void SetDiscountByNumber(decimal? value)
    {
        DiscountByNumber = value;
    }

    public void SetDiscountedPrice(decimal? value)
    {
        DiscountedPrice = value;
    }

    public void SetFinalPrice(decimal? value)
    {
        FinalPrice = value;
    }

    public void SetPackageUnitPrice(decimal? value)
    {
        PackageUnitPrice = value;
    }

    public void SetPackingPrice(decimal? value)
    {
        PackingPrice = value;
    }

    public void SetPackageCount(decimal? value)
    {
        PackageCount = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetManagementDescription(string? value)
    {
        ManagementDescription = value;
    }

    public void SetCustomerInvoiceNumber(string? value)
    {
        CustomerInvoiceNumber = value;
    }

    public void SetStatus(RGSTypeStatus value)
    {
        Status = value;
    }

    public void SetImportance(GoodsSupplyDetailImportance value)
    {
        Importance = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RequestGoodsSupplyTypeHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}