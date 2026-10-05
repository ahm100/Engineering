using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeDetailHistory : AuditableEntity<RequestGoodsSupplyTypeDetailHistory>
{
    [Description(RGSCmts.Importance)]
    public GoodsSupplyDetailImportance? Importance { get; private set; } = GoodsSupplyDetailImportance.Lowest;

    [Description(RGSCmts.ReferenceId)]
    public long? ReferenceId { get; private set; }

    [Description(RGSCmts.SupplyType)]
    public SupplyType Type { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; } = 0;

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DelivaryDeadLine { get; private set; }

    [Description(RGSCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.PackingPrice)]
    public decimal? PackingPrice { get; private set; }

    [Description(RGSCmts.FinalPrice)]
    public decimal? FinalPrice { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.ManagementDescription)]
    public string? ManagementDescription { get; private set; }

    [Description(RGSCmts.CheckGroup)]
    public bool CheckGroup { get; private set; } = false;

    [Description(GlobalCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(GlobalCmts.PackageId)]
    public long? PackageId { get; private set; }

    [Description(RGSCmts.PackageCount)]
    public decimal? PackageCount { get; private set; }

    [Description(RGSCmts.PackageUnitPrice)]
    public decimal? PackageUnitPrice { get; private set; }

    [Description(GlobalCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(GlobalCmts.LastDescription)]
    public long RequestGoodsSupplyTypeDetailId { get; private set; }
    public RequestGoodsSupplyTypeDetail RequestGoodsSupplyTypeDetail { get; private set; }

    public RequestGoodsSupplyTypeDetailHistory(
        RequestGoodsSupplyTypeDetail rgsTypeDetail) : this()
    {
        SetRequestGoodsSupplyTypeDetail(rgsTypeDetail);
        SetRequestedCount(rgsTypeDetail.RequestedCount);
        SetReferenceId(rgsTypeDetail.ReferenceId);
        SetSupplyType(rgsTypeDetail.Type);
        SetCheckGroup(rgsTypeDetail.CheckGroup);
        SetImportance(rgsTypeDetail.Importance);

        SetDelivaryDeadLine(rgsTypeDetail.DelivaryDeadLine);

        SetUnitPrice(rgsTypeDetail.UnitPrice);
        SetTotalPrice(rgsTypeDetail.TotalPrice);

        SetPackingPrice(rgsTypeDetail.PackingPrice);
        SetFinalPrice(rgsTypeDetail.FinalPrice);

        SetContractorId(rgsTypeDetail.ContractorId);

        SetPackageId(rgsTypeDetail.PackageId);
        SetPackageCount(rgsTypeDetail.PackageCount);
        SetPackageUnitPrice(rgsTypeDetail.PackageUnitPrice);

        SetDescription(rgsTypeDetail.Description);
        SetManagementDescription(rgsTypeDetail.ManagementDescription);
        SetLastDescription(rgsTypeDetail.LastDescription);
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestGoodsSupplyTypeDetail(RequestGoodsSupplyTypeDetail value)
    {
        RequestGoodsSupplyTypeDetail = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyTypeDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetPackageId(long? value)
    {
        PackageId = value;
    }

    public void SetCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void SetPackageCount(decimal? value)
    {
        PackageCount = value;
    }

    public void SetDelivaryDeadLine(DateTime? value)
    {
        DelivaryDeadLine = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
    }

    public void SetUnitPrice(decimal? value)
    {
        UnitPrice = value;
    }

    public void SetPackingPrice(decimal? value)
    {
        PackingPrice = value;
    }

    public void SetReferenceId(long? value)
    {
        ReferenceId = value;
    }

    public void SetSupplyType(SupplyType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetFinalPrice(decimal? value)
    {
        FinalPrice = value;
    }

    public void SetPackageUnitPrice(decimal? value)
    {
        PackageUnitPrice = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetManagementDescription(string? value)
    {
        ManagementDescription = value;
    }

    public void SetImportance(GoodsSupplyDetailImportance? value)
    {
        Importance = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RequestGoodsSupplyTypeDetailHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}