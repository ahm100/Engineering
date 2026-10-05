using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;

/// <summary>
/// فهرست کالاهای ثبت شده درخواست تامین کالا
/// </summary>
public class RequestGoodsSupplyDetail : AuditableEntity<RequestGoodsSupplyDetail>
{
    [Description(RGSCmts.Importance)]
    public GoodsSupplyDetailImportance? Importance { get; private set; } = GoodsSupplyDetailImportance.Lowest;

    [Description(RGSCmts.Status)]
    public GoodsSupplyDetailStatus Status { get; private set; } = GoodsSupplyDetailStatus.New;

    [Description(RGSCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; } = 0;

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DelivaryDeadLine { get; private set; }

    [Description(RGSCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.DiscountedPrice)]
    public decimal? DiscountedPrice { get; private set; }

    [Description(RGSCmts.TaxPercentage)]
    public decimal? TaxPercentage { get; private set; }

    [Description(RGSCmts.TaxNumber)]
    public decimal? TaxNumber { get; private set; }

    [Description(RGSCmts.DiscountByNumber)]
    public decimal? DiscountByNumber { get; private set; }

    [Description(RGSCmts.DiscountByPercentage)]
    public decimal? DiscountByPercentage { get; private set; }

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

    [Description(GlobalCmts.DestinationWarehouseId)]
    public long? DestinationWarehouseId { get; private set; }

    [Description(RGSCmts.CustomerInvoiceNumber)]
    public string? CustomerInvoiceNumber { get; private set; }

    [Description(GlobalCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(RGSCmts.ConsumableVolumeProduct)]
    public long? ConsumableVolumeProductId { get; private set; }
    public ConsumableVolumeProduct? ConsumableVolumeProduct { get; private set; }

    [Description(ProjectCmts.ProjectProduct)]
    public long? ProjectProductId { get; private set; }
    public ProjectProduct? ProjectProduct { get; private set; }

    [Description(RGSCmts.OperationInfoSeason)]
    public long RequestGoodsSupplyId { get; private set; }
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyProduct)]
    public long? RequestGoodsSupplyProductId { get; private set; }
    public RequestGoodsSupplyProduct? RequestGoodsSupplyProduct { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }

    private RequestGoodsSupplyDetail(CreateRGSDetailParameters parameters) : this()
    {
        RequestGoodsSupply = Guard.Against.Null(parameters.RequestGoodsSupply, nameof(parameters.RequestGoodsSupply));

        if (RequestGoodsSupply.IsProjectSupply)
        {
            ProjectProduct = Guard.Against.Null(parameters.ProjectProduct, nameof(parameters.ProjectProduct));
            ProjectProductId = Guard.Against.Null(parameters.ProjectProduct.Id, nameof(parameters.ProjectProduct.Id));
        }
        else
        {
            ConsumableVolumeProduct = Guard.Against.Null(parameters.ConsumableVolumeProduct, nameof(parameters.ConsumableVolumeProduct));
        }

        RequestGoodsSupplyProduct = parameters.RequestGoodsSupplyProduct;

        Status = RequestGoodsSupply.Status == GoodsSupplyStatus.Draft ?
            GoodsSupplyDetailStatus.Draft : GoodsSupplyDetailStatus.New;

        SetCostCenter(parameters.CostCenter);
        SetRequestedCount(parameters.RequestedCount);
        SetProductId(parameters.ProductId);
        SetCheckGroup(parameters.CheckGroup);
        SetImportance(parameters.Importance);

        SetDelivaryDeadLine(parameters.DelivaryDeadLine);

        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);

        SetDiscountByNumber(parameters.DiscountByNumber);
        SetDiscountByPercentage(parameters.DiscountByPercentage);
        SetDiscountedPrice(parameters.DiscountedPrice);

        SetTaxNumber(parameters.TaxNumber);
        SetTaxPercentage(parameters.TaxPercentage);

        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);

        SetContractorId(parameters.ContractorId);

        SetPackageId(parameters.PackageId);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);

        SetCustomerInvoiceNumber(parameters.CustomerInvoiceNumber);

        AddDocuments(parameters.DocumentUrls);

        SetDescription(parameters.Description);
        SetManagementDescription(parameters.ManagementDescription);
        SetDestinationWarehouseId(parameters.DestinationWarehouseId);
        SetLastDescription(parameters.LastDescription);

        AddHistory();
    }

    public static RequestGoodsSupplyDetail Create(CreateRGSDetailParameters parameters)
    {
        return new RequestGoodsSupplyDetail(parameters);
    }

    public void UpdateDetail(UpdateRGSDetailParameters parameters)
    {
        SetRequestedCount(Math.Round(parameters.RequestedCount, 3));

        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);

        SetDiscountByNumber(parameters.DiscountByNumber);
        SetDiscountByPercentage(parameters.DiscountByPercentage);
        SetDiscountedPrice(parameters.DiscountedPrice);

        SetTaxNumber(parameters.TaxNumber);
        SetTaxPercentage(parameters.TaxPercentage);

        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);

        SetDelivaryDeadLine(parameters.DelivaryDeadLine);

        SetDescription(parameters.Description);
        SetManagementDescription(parameters.ManagementDescription);

        UpdateCheckGroup(parameters.CheckGroup);

        SetContractorId(parameters.ContractorId);

        SetDestinationWarehouseId(parameters.DestinationWarehouseId);

        SetPackageId(parameters.PackageId);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);

        SetImportance(parameters.Importance);

        SetCustomerInvoiceNumber(parameters.CustomerInvoiceNumber);

        AddDocuments(parameters.DocumentUrls);

        SetProduct(parameters.RequestGoodsSupplyProduct);

        AddHistory();
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetCostCenter(CostCenter? value)
    {
        CostCenter = value;
        CostCenterId = value?.Id;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetPackageId(long? value)
    {
        PackageId = value;
    }

    public void SetDestinationWarehouseId(long? value)
    {
        DestinationWarehouseId = value;
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

    public void SetStatusToDraft()
    {
        Status = GoodsSupplyDetailStatus.Draft;
    }

    public void SetStatusToNew()
    {
        Status = GoodsSupplyDetailStatus.New;
        AddHistory();
    }

    public void SetStatus(GoodsSupplyDetailStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
        AddHistory();
    }

    public void SetTaxPercentage(decimal? value)
    {
        TaxPercentage = value;
    }

    public void SetDiscountByNumber(decimal? value)
    {
        DiscountByNumber = value;
    }

    public void SetProductId(long value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetDiscountByPercentage(decimal? value)
    {
        DiscountByPercentage = value;
    }

    public void SetDiscountedPrice(decimal? value)
    {
        DiscountedPrice = value;
    }

    public void SetFinalPrice(decimal? value)
    {
        FinalPrice = value;
    }

    public void SetTaxNumber(decimal? value)
    {
        TaxNumber = value;
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
        AddHistory();
    }

    public void SetCustomerInvoiceNumber(string? value)
    {
        CustomerInvoiceNumber = value;
    }

    public void SetProduct(RequestGoodsSupplyProduct? value)
    {
        RequestGoodsSupplyProduct = value;
    }

    public void SetImportance(GoodsSupplyDetailImportance? value)
    {
        Importance = value;
    }

    public void UpdateStatus(GoodsSupplyDetailStatus value, string? lastDescription)
    {
        Status = value;
        LastDescription = lastDescription;
        AddHistory();
    }

    public void UpdateStatus(GoodsSupplyDetailStatus value, long? userId, string? lastDescription)
    {
        Status = Guard.Against.Null(value, nameof(value));
        LastDescription = lastDescription;
        AddHistory(userId, lastDescription);
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void UpdateCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void UpdateStatus(RequestGoodsSupplyDetail value, long? userId, string? lastDescription)
    {

        Status = StatusChecker(value);
        SetLastDescription(lastDescription);
        AddHistory(userId, lastDescription);
    }

    public GoodsSupplyDetailStatus StatusChecker(RequestGoodsSupplyDetail detail)
    {
        GoodsSupplyDetailStatus status = GoodsSupplyDetailStatus.PendingForSupply;

        if (detail.RequestGoodsSupplyManagements.All(oo => oo.Status == GoodsSupplyManagementStatus.Return))
            status = GoodsSupplyDetailStatus.NotCompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.All(oo => oo.Status == GoodsSupplyManagementStatus.CompleteSupply))
            status = GoodsSupplyDetailStatus.CompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.All(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.ReturnToSupply;

        else if (detail.RequestGoodsSupplyManagements.Any(oo => oo.Status == GoodsSupplyManagementStatus.InCompleteSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.Any(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.Any(oo => oo.Status == GoodsSupplyManagementStatus.Return))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.Any(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (detail.RequestGoodsSupplyManagements.Any(oo => oo.Status == GoodsSupplyManagementStatus.CompleteSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;

        else
            status = GoodsSupplyDetailStatus.PendingForSupply;

        return status;
    }

    public void AddHistory()
    {
        _requestGoodsSupplyDetailHistories.Add(RequestGoodsSupplyDetailHistory.Create(this,
            Status,
            RequestedCount,
            UnitPrice,
            TotalPrice,
            DiscountByNumber,
            DiscountByPercentage,
            DiscountedPrice,
            TaxNumber,
            TaxPercentage,
            FinalPrice,
            FinalPrice,
            DelivaryDeadLine,
            Description,
            ManagementDescription,
            CustomerInvoiceNumber));
    }

    public void AddHistory(long? userId, string? description)
    {
        _requestGoodsSupplyDetailHistories.Add(RequestGoodsSupplyDetailHistory.Create(this,
            Status,
            RequestedCount,
            UnitPrice,
            TotalPrice,
            DiscountByNumber,
            DiscountByPercentage,
            DiscountedPrice,
            TaxNumber,
            TaxPercentage,
            PackingPrice,
            FinalPrice,
            DelivaryDeadLine,
            userId,
            description,
            ManagementDescription,
            CustomerInvoiceNumber));
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            _requestGoodsSupplyDetailDocuments.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _requestGoodsSupplyDetailDocuments.Add(RequestGoodsSupplyDetailDocument.Create(url, this));
        }
        else
            _requestGoodsSupplyDetailDocuments.ForEach(c => c.SetIsDeleted());
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<RequestGoodsSupplyDetailDocument> _requestGoodsSupplyDetailDocuments;
    public IReadOnlyList<RequestGoodsSupplyDetailDocument> RequestGoodsSupplyDetailDocuments => _requestGoodsSupplyDetailDocuments;

    private List<RequestGoodsSupplyDetailHistory> _requestGoodsSupplyDetailHistories;
    public IReadOnlyList<RequestGoodsSupplyDetailHistory> RequestGoodsSupplyDetailHistories => _requestGoodsSupplyDetailHistories;

    private List<RequestGoodsSupplyManagement> _requestGoodsSupplyManagements;
    public IReadOnlyList<RequestGoodsSupplyManagement> RequestGoodsSupplyManagements => _requestGoodsSupplyManagements;

    private List<ContractorStatusStatementProduct> _contractorStatusStatementServiceProducts;
    public IReadOnlyList<ContractorStatusStatementProduct> ContractorStatusStatementServiceProducts => _contractorStatusStatementServiceProducts;

    private RequestGoodsSupplyDetail()
    {
        _requestGoodsSupplyDetailHistories = [];
        _requestGoodsSupplyDetailDocuments = [];
        _requestGoodsSupplyManagements = [];
        _contractorStatusStatementServiceProducts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
