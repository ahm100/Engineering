using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;

[Description(RGSCmts.RequestGoodsSupply)]
public class RequestGoodsSupply : ActivateEntity<RequestGoodsSupply>
{
    [Description(RGSCmts.RequestSerialNumber)]
    public string RequestSerialNumber => $"{SerialNumber}-{this.Id}";

    [Description(RGSCmts.SerialNumber)]
    public string SerialNumber { get; private set; }

    [Description(RGSCmts.Status)]
    public GoodsSupplyStatus Status { get; private set; } = GoodsSupplyStatus.Created;

    [Description(RGSCmts.Type)]
    public GoodsSupplyType Type { get; private set; }

    [Description(RGSCmts.PurchaseLocation)]
    public PurchaseLocation? PurchaseLocation { get; private set; }

    [Description(RGSCmts.PurchaseReason)]
    public PurchaseReason? PurchaseReason { get; private set; }

    [Description(RGSCmts.SupplyerId)]
    public long? SupplyerId { get; private set; }

    [Description(RGSCmts.BuyerId)]
    public long? BuyerId { get; private set; }

    [Description(RGSCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(RGSCmts.TransferPrice)]
    public decimal? TransferPrice { get; private set; }

    [Description(RGSCmts.OtherPrice)]
    public decimal? OtherPrice { get; private set; }

    [Description(RGSCmts.DiscountOnInvoicePercentage)]
    public decimal? DiscountOnInvoicePercentage { get; private set; }

    [Description(RGSCmts.DiscountOnInvoiceNumber)]
    public decimal? DiscountOnInvoiceNumber { get; private set; }

    [Description(RGSCmts.DiscountedPriceOnInvoice)]
    public decimal? DiscountedPriceOnInvoice { get; private set; }

    [Description(RGSCmts.TaxOnInvoicePercentage)]
    public decimal? TaxOnInvoicePercentage { get; private set; }

    [Description(RGSCmts.TaxOnInvoiceNumber)]
    public decimal? TaxOnInvoiceNumber { get; private set; }

    [Description(RGSCmts.FinalInvoiceAmount)]
    public decimal? FinalInvoiceAmount { get; private set; }

    [Description(RGSCmts.RequestedDate)]
    public DateTime? RequestedDate { get; private set; }

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DeliveryDeadline { get; private set; }

    [Description(RGSCmts.RegistrationNumber)]
    public string? RegistrationNumber { get; private set; }

    [Description(RGSCmts.RequestingOrganizationId)]
    public long? RequestingOrganizationId { get; private set; }

    [Description(RGSCmts.DescriptionEn)]
    public string? DescriptionEn { get; private set; }

    [Description(RGSCmts.IsPettyCash)]
    public bool? IsPettyCash { get; private set; } = false;

    [Description(RGSCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.DeviceName)]
    public string? DeviceName { get; private set; }

    [Description(RGSCmts.DeviceEnName)]
    public string? DeviceEnName { get; private set; }

    [Description(RGSCmts.DeviceNumber)]
    public string? DeviceNumber { get; private set; }

    [Description(RGSCmts.DeviceCode)]
    public string? DeviceCode { get; private set; }

    [Description(RGSCmts.DeviceCode)]
    public string? UnitCode { get; private set; }

    [Description(RGSCmts.ServiceReasonType)]
    public ServiceReasonType? ServiceReasonType { get; private set; }

    [Description(RGSCmts.Importance)]
    public GoodsSupplyDetailImportance? Importance { get; private set; } = GoodsSupplyDetailImportance.Lowest;

    [Description(RGSCmts.ConsumptionRateAndInventoryUrl)]
    public string? ConsumptionRateAndInventoryUrl { get; private set; }

    [Description(RGSCmts.ConsumptionAddress)]
    public string? ConsumptionAddress { get; private set; }

    [Description(RGSCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(RGSCmts.IsProjectSupply)]
    public bool IsProjectSupply { get; private set; }

    [Description(RGSCmts.IsArchived)]
    public bool IsArchived { get; private set; } = false;

    [Description(RGSCmts.Project)]
    public long? ProjectId { get; private set; }
    public Project? Project { get; private set; }

    [Description(RGSCmts.ProjectOperation)]
    public long? ProjectOperationId { get; private set; }
    public ProjectOperation? ProjectOperation { get; private set; }

    [Description(RGSCmts.OperationInfoSeason)]
    public long? OperationInfoSeasonId { get; private set; }
    public OperationInfoSeason? OperationInfoSeason { get; private set; }

    [Description(RGSCmts.ProjectOperationDetail)]
    public long? ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail? ProjectOperationDetail { get; private set; }

    [Description(RGSCmts.ProjectOperationDetail)]
    public long? ParentId { get; private set; }
    public RequestGoodsSupply? Parent { get; private set; }

    private RequestGoodsSupply(CreateRGSParameters parameters) : this()
    {
        SetIsProjectSupply(parameters.IsProjectSupply);

        if (parameters.IsProjectSupply)
        {
            SetProject(parameters.Project);
            SetSerialNumber(parameters.ConfigCode ?? parameters.Project!.Prefix ?? $"{parameters.Project.ProjectCode}");
        }
        else
        {
            SetProjectOperation(parameters.ProjectOperation);
            SetSerialNumber(parameters.ConfigCode ?? ProjectOperation!.Project.Prefix ?? $"{ProjectOperation.Project.ProjectCode}");
        }

        SetParent(parameters.Parent);

        SetProjectOperationDetail(parameters.ProjectOperationDetail);
        SetOperationInfoSeason(parameters.OperationInfoSeason);

        SetType(parameters.Type);
        SetStatus(parameters.Status);

        SetSupplyerId(parameters.SupplierId);
        SetBuyerId(parameters.BuyerId);
        SetCompanyId(parameters.CompanyId);

        SetCurrencyId(parameters.CurrencyId);
        SetTransferPrice(parameters.TransferPrice);
        SetOtherPrice(parameters.OtherPrice);

        SetDiscountOnInvoicePercentage(parameters.DiscountOnInvoicePercentage);
        SetDiscountOnInvoiceNumber(parameters.DiscountOnInvoiceNumber);
        SetDiscountedPriceOnInvoice(parameters.DiscountedPriceOnInvoice);

        SetTaxOnInvoicePercentage(parameters.TaxOnInvoicePercentage);
        SetTaxOnInvoiceNumber(parameters.TaxOnInvoiceNumber);
        SetFinalInvoiceAmount(parameters.FinalInvoiceAmount);

        SetRequestedDate(parameters.RequestedDate);
        SetIsPettyCash(parameters.IsPettyCash);

        SetDescription(parameters.Description);

        SetConsumptionAddress(parameters.ConsumptionAddress);
        SetConsumptionRateAndInventoryUrl(parameters.ConsumptionRateAndInventoryUrl);

        SetPurchaseLocation(parameters.PurchaseLocation);
        SetPurchaseReason(parameters.PurchaseReason);

        SetDeliveryDeadline(parameters.DeliveryDeadline);
        SetRegistrationNumber(parameters.RegistrationNumber);
        SetRequestingOrganizationId(parameters.RequestingOrganizationId);
        SetDescriptionEn(parameters.DescriptionEn);

        SetDeviceName(parameters.DeviceName);
        SetDeviceEnName(parameters.DeviceEnName);
        SetDeviceCode(parameters.DeviceCode);
        SetDeviceNumber(parameters.DeviceNumber);
        SetServiceReasonType(parameters.ServiceReasonType);
        SetUnitCode(parameters.UnitCode);
        SetActive();

        AddHistory();
    }

    public static RequestGoodsSupply Create(CreateRGSParameters parameters)
    {
        return new(parameters);
    }

    public void Update(UpdateRGSParameters parameters)
    {
        SetSupplyerId(parameters.SupplierId);
        SetBuyerId(parameters.BuyerId);

        SetCurrencyId(parameters.CurrencyId);
        SetTransferPrice(parameters.TransferPrice);
        SetOtherPrice(parameters.OtherPrice);

        SetDiscountOnInvoicePercentage(parameters.DiscountOnInvoicePercentage);
        SetDiscountOnInvoiceNumber(parameters.DiscountOnInvoiceNumber);
        SetDiscountedPriceOnInvoice(parameters.DiscountedPriceOnInvoice);

        SetTaxOnInvoicePercentage(parameters.TaxOnInvoicePercentage);
        SetTaxOnInvoiceNumber(parameters.TaxOnInvoiceNumber);

        SetRequestedDate(parameters.RequestedDate);
        SetDeliveryDeadline(parameters.DeliveryDeadline);

        SetIsPettyCash(parameters.IsPettyCash);

        SetDescription(parameters.Description);
        SetDescriptionEn(parameters.DescriptionEn);

        SetRegistrationNumber(parameters.RegistrationNumber);
        SetRequestingOrganizationId(parameters.RequestingOrganizationId);

        SetPurchaseLocation(parameters.PurchaseLocation);
        SetPurchaseReason(parameters.PurchaseReason);

        SetDeviceName(parameters.DeviceName);
        SetDeviceEnName(parameters.DeviceEnName);
        SetDeviceCode(parameters.DeviceCode);
        SetDeviceNumber(parameters.DeviceNumber);
        SetUnitCode(parameters.UnitCode);
        SetServiceReasonType(parameters.ServiceReasonType);

        SetConsumptionAddress(parameters.ConsumptionAddress);
        SetConsumptionRateAndInventoryUrl(parameters.ConsumptionRateAndInventoryUrl);
        SetIsArchived(parameters.IsArchived);
        if (parameters.IsArchived == true)
            SetStatus(GoodsSupplyStatus.Archive);
        SetParent(parameters.Parent);

        Status = parameters.IsDraft
            ? GoodsSupplyStatus.Draft
            : GoodsSupplyStatus.Created;
    }

    public void UpdateStatus(GoodsSupplyStatus status)
    {
        ArgumentNullException.ThrowIfNull(status);

        Status = Guard.Against.EnumOutOfRange(status);
        AddHistory();
    }

    public void SetStatus(GoodsSupplyStatus value, string? description)
    {
        Status = value;
        if (!string.IsNullOrEmpty(description))
            SetDescription(description);

        AddHistory();
    }

    public void SetStatusToDraft()
    {
        Status = GoodsSupplyStatus.Draft;
    }

    public void SetServiceReasonType(ServiceReasonType? value)
    {
        ServiceReasonType = value;
    }

    public void SetStatusToCreated()
    {
        Status = GoodsSupplyStatus.Created;
    }

    public void SetIsDelete()
    {
        IsDeleted = true;
    }

    public void SetIsArchived(bool? value)
    {
        IsArchived = value ?? false;
    }

    public void SetConsumptionAddress(string? value)
    {
        ConsumptionAddress = value;
    }

    public void SetParent(RequestGoodsSupply? value)
    {
        Parent = value;
        ParentId = value?.Id;
    }

    public void SetDeliveryDeadline(DateTime? value)
    {
        DeliveryDeadline = value;
    }

    public void SetRegistrationNumber(string? value)
    {
        RegistrationNumber = value;
    }

    public void SetDeviceName(string? value)
    {
        DeviceName = value;
    }

    public void SetDeviceEnName(string? value)
    {
        DeviceEnName = value;
    }

    public void SetDeviceNumber(string? value)
    {
        DeviceNumber = value;
    }

    public void SetDeviceCode(string? value)
    {
        DeviceCode = value;
    }

    public void SetUnitCode(string? value)
    {
        UnitCode = value;
    }

    public void SetRequestingOrganizationId(long? value)
    {
        RequestingOrganizationId = value;
    }

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }

    public void SetConsumptionRateAndInventoryUrl(string? value)
    {
        ConsumptionRateAndInventoryUrl = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetTransferPrice(decimal? value)
    {
        TransferPrice = value;
    }

    public void SetOperationInfoSeason(OperationInfoSeason? value)
    {
        OperationInfoSeason = value;
    }

    public void SetOtherPrice(decimal? value)
    {
        OtherPrice = value;
    }

    public void SetIsProjectSupply(bool isProjectSupply)
    {
        IsProjectSupply = isProjectSupply;
    }

    public void SetProject(Project? project)
    {
        Project = project;
        ProjectId = project?.Id;
    }

    public void SetProjectOperation(ProjectOperation? projectOperation)
    {
        ProjectOperation = projectOperation;
        ProjectOperationId = projectOperation?.Id;
    }

    public void SetProjectOperationDetail(ProjectOperationDetail? projectOperationDetail)
    {
        ProjectOperationDetail = projectOperationDetail;
        ProjectOperationDetailId = projectOperationDetail?.Id;
    }

    public void SetDiscountOnInvoicePercentage(decimal? value)
    {
        DiscountOnInvoicePercentage = value;
    }

    public void SetDiscountOnInvoiceNumber(decimal? value)
    {
        DiscountOnInvoiceNumber = value;
    }

    public void SetDiscountedPriceOnInvoice(decimal? value)
    {
        DiscountedPriceOnInvoice = value;
    }

    public void SetTaxOnInvoicePercentage(decimal? value)
    {
        TaxOnInvoicePercentage = value;
    }

    public void SetTaxOnInvoiceNumber(decimal? value)
    {
        TaxOnInvoiceNumber = value;
    }

    public void SetFinalInvoiceAmount(decimal? value)
    {
        FinalInvoiceAmount = value;
    }

    public void SetIsPettyCash(bool? value)
    {
        IsPettyCash = value;
    }

    public void SetRequestedDate(DateTime? value)
    {
        RequestedDate = value;
    }

    public void SetSupplyerId(long? value)
    {
        SupplyerId = value;
    }

    public void SetBuyerId(long? value)
    {
        BuyerId = value;
    }

    public void SetCurrencyId(long? value)
    {
        CurrencyId = value;
    }

    public void AddHistory()
    {
        _requestGoodsSupplyHistories.Add(RequestGoodsSupplyHistory.Create(this, this.Status, this.Description));
    }

    public void AddHistory(long? userId, string? description)
    {
        _requestGoodsSupplyHistories.Add(RequestGoodsSupplyHistory.Create(this, this.Status, description, userId));
    }

    public void ChangeStatus(RequestGoodsSupply value, string? description, long? userId)
    {
        AddHistory(userId, description);
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetPurchaseLocation(PurchaseLocation? value)
    {
        PurchaseLocation = value;
    }

    public void SetPurchaseReason(PurchaseReason? value)
    {
        PurchaseReason = value;
    }

    public void SetSerialNumber(string serialNumber)
    {
        SerialNumber = Guard.Against.NullOrWhiteSpace(
            serialNumber,
            nameof(serialNumber));
    }

    public void SetStatus(GoodsSupplyStatus status)
    {
        Status = Guard.Against.Null(status, nameof(status));
    }

    public void SetType(GoodsSupplyType type)
    {
        Type = Guard.Against.Null(type, nameof(type));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<RequestGoodsSupplyDetail> _requestGoodsSupplyDetail;
    public IReadOnlyList<RequestGoodsSupplyDetail> RequestGoodsSupplyDetails => _requestGoodsSupplyDetail;

    private List<RequestGoodsSupplyProduct> _requestGoodsSupplyProducts;
    public IReadOnlyList<RequestGoodsSupplyProduct> RequestGoodsSupplyProducts => _requestGoodsSupplyProducts;

    private List<RequestGoodsSupplyHistory> _requestGoodsSupplyHistories;
    public IReadOnlyList<RequestGoodsSupplyHistory> RequestGoodsSupplyHistories => _requestGoodsSupplyHistories;

    private List<RequestGoodsSupplyDocument> _requestGoodsSupplyDocument;
    public IReadOnlyList<RequestGoodsSupplyDocument> RequestGoodsSupplyDocuments => _requestGoodsSupplyDocument;

    private List<RequestGoodsSupplyType> _requestGoodsSupplyTypes;
    public IReadOnlyList<RequestGoodsSupplyType> RequestGoodsSupplyTypes => _requestGoodsSupplyTypes;

    private List<RequestGoodsSupplyTypeDetail> _requestGoodsSupplyTypeDetails;
    public IReadOnlyList<RequestGoodsSupplyTypeDetail> RequestGoodsSupplyTypeDetails => _requestGoodsSupplyTypeDetails;

    private List<RequestGoodsSupply> _childs;
    public IReadOnlyList<RequestGoodsSupply> Childs => _childs;

    private RequestGoodsSupply()
    {
        _requestGoodsSupplyDetail = [];
        _requestGoodsSupplyProducts = [];
        _requestGoodsSupplyHistories = [];
        _requestGoodsSupplyDocument = [];
        _requestGoodsSupplyTypes = [];
        _requestGoodsSupplyTypeDetails = [];
        _childs = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
