using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;

public class RequestGoodsSupplyTypeDetail : AuditableEntity<RequestGoodsSupplyTypeDetail>
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

    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; }

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

    [Description(ProjectCmts.ProjectProduct)]
    public long? ProjectProductId { get; private set; }
    public ProjectProduct? ProjectProduct { get; private set; }

    [Description(RGSCmts.RequestGoodsSupply)]
    public long RequestGoodsSupplyId { get; private set; }
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyType)]
    public long? RequestGoodsSupplyTypeId { get; private set; }
    public RequestGoodsSupplyType RequestGoodsSupplyType { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }

    private RequestGoodsSupplyTypeDetail(CreateRGSTypeDetailParameters parameters) : this()
    {
        SetRequestGoodsSupply(parameters.RequestGoodsSupply);

        SetProjectProduct(parameters.ProjectProduct);

        SetRequestGoodsSupplyType(parameters.RequestGoodsSupplyType);

        SetCostCenter(parameters.CostCenter);
        SetRequestedCount(parameters.RequestedCount);
        SetReferenceId(parameters.ReferenceId);
        SetSupplyType(parameters.Type);
        SetCheckGroup(parameters.CheckGroup);
        SetImportance(parameters.Importance);

        SetDelivaryDeadLine(parameters.DelivaryDeadLine);

        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);

        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);

        SetContractorId(parameters.ContractorId);

        SetPackageId(parameters.PackageId);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);

        AddDocuments(parameters.DocumentUrls);

        SetDescription(parameters.Description);
        SetDescriptionEn(parameters.DescriptionEn);
        SetManagementDescription(parameters.ManagementDescription);
        SetLastDescription(parameters.LastDescription);

        AddHistory();
    }

    public static RequestGoodsSupplyTypeDetail Create(CreateRGSTypeDetailParameters parameters)
    {
        return new RequestGoodsSupplyTypeDetail(parameters);
    }

    public void Update(UpdateRGSTypeDetailParameters parameters)
    {
        SetRequestedCount(Math.Round(parameters.RequestedCount, 3));

        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);

        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);

        SetDelivaryDeadLine(parameters.DelivaryDeadLine);

        SetDescription(parameters.Description);
        SetDescriptionEn(parameters.DescriptionEn);
        SetManagementDescription(parameters.ManagementDescription);

        UpdateCheckGroup(parameters.CheckGroup);

        SetContractorId(parameters.ContractorId);

        SetPackageId(parameters.PackageId);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);

        SetImportance(parameters.Importance);

        AddDocuments(parameters.DocumentUrls);

        SetCostCenter(parameters.CostCenter);

        AddHistory();
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetRequestGoodsSupply(RequestGoodsSupply value)
    {
        RequestGoodsSupply = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCostCenter(CostCenter? value)
    {
        CostCenter = value;
        CostCenterId = value?.Id;
    }

    public void SetProjectProduct(ProjectProduct? value)
    {
        ProjectProduct = value;
        ProjectProductId = value?.Id;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.Null(value, nameof(value));
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

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }

    public void SetManagementDescription(string? value)
    {
        ManagementDescription = value;
        AddHistory();
    }

    public void SetRequestGoodsSupplyType(RequestGoodsSupplyType value)
    {
        RequestGoodsSupplyType = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyTypeId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetImportance(GoodsSupplyDetailImportance? value)
    {
        Importance = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void UpdateCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void AddHistory()
    {
        _requestGoodsSupplyTypeDetailHistories.Add(new RequestGoodsSupplyTypeDetailHistory(this));
    }

    public void AddHistory(long? userId, string? description)
    {
        //_requestGoodsSupplyDetailHistories.Add(RequestGoodsSupplyDetailHistory.Create(this,
        //    Status,
        //    RequestedCount,
        //    UnitPrice,
        //    TotalPrice,
        //    DiscountByNumber,
        //    DiscountByPercentage,
        //    DiscountedPrice,
        //    TaxNumber,
        //    TaxPercentage,
        //    PackingPrice,
        //    FinalPrice,
        //    DelivaryDeadLine,
        //    userId,
        //    description,
        //    ManagementDescription,
        //    CustomerInvoiceNumber));
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            _requestGoodsSupplyTypeDetailDocuments.ForEach(c => c.SoftDelete());

            foreach (var url in urls)
                _requestGoodsSupplyTypeDetailDocuments.Add(RequestGoodsSupplyTypeDetailDocument.Create(url, this));
        }
        else
            _requestGoodsSupplyTypeDetailDocuments.ForEach(c => c.SoftDelete());
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<RequestGoodsSupplyTypeDetailDocument> _requestGoodsSupplyTypeDetailDocuments;
    public IReadOnlyList<RequestGoodsSupplyTypeDetailDocument> RequestGoodsSupplyTypeDetailDocuments => _requestGoodsSupplyTypeDetailDocuments;
    private List<RequestGoodsSupplyTypeDetailHistory> _requestGoodsSupplyTypeDetailHistories;
    public IReadOnlyList<RequestGoodsSupplyTypeDetailHistory> RequestGoodsSupplyTypeDetailHistories => _requestGoodsSupplyTypeDetailHistories;
    private RequestGoodsSupplyTypeDetail()
    {
        _requestGoodsSupplyTypeDetailDocuments = [];
        _requestGoodsSupplyTypeDetailHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
