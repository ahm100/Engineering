using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;
public class RequestGoodsSupplyType : AuditableEntity<RequestGoodsSupplyType>
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

    [Description(RGSCmts.SerialNumber)]
    public string? ProjectName { get; private set; }

    [Description(RGSCmts.SerialNumber)]
    public string? ProjectCode { get; private set; }

    [Description(RGSCmts.SerialNumber)]
    public string? ProjectEnName { get; private set; }

    [Description(RGSCmts.IsHistoryAdded)]
    [NotMapped]
    public bool IsHistoryAdded { get; set; } = false;

    [Description(RGSCmts.RequestGoodsSupply)]
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }
    public long RequestGoodsSupplyId { get; private set; }

    public RequestGoodsSupplyType(CreateRGSTypeParameters parameters) : this()
    {
        SetRequestGoodsSupply(parameters.RequestGoodsSupply);

        Status = RGSTypeStatus.Requested;

        SetImportance(parameters.Importance);
        SetDelivaryDeadLine(parameters.DelivaryDeadLine);
        SetReferenceId(parameters.ReferenceId);
        SetSupplyType(parameters.Type);
        SetPackageId(parameters.PackageId);
        SetRequestedCount(parameters.RequestedCount);
        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);
        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);
        SetCheckGroup(parameters.CheckGroup);
        SetContractorId(parameters.ContractorId);
        SetDescription(parameters.Description);
        SetManagementDescription(parameters.ManagementDescription);
        SetProjectName(parameters.ProjectName);
        SetProjectEnName(parameters.ProjectEnName);
        SetProjectCode(parameters.ProjectCode);

        SerialNumber = $"{RequestGoodsSupply.Project!.ProjectCode}";

        IsHistoryAdded = parameters.IsHistoryAdded;
        AddDocuments(parameters.Urls);
        AddHistory();
    }

    public void Update(UpdateRGSTypeParameters parameters)
    {
        SetImportance(parameters.Importance);
        SetDelivaryDeadLine(parameters.DelivaryDeadLine);
        SetReferenceId(parameters.ReferenceId);
        SetPackageId(parameters.PackageId);
        SetRequestedCount(parameters.RequestedCount);
        SetUnitPrice(parameters.UnitPrice);
        SetTotalPrice(parameters.TotalPrice);
        SetPackingPrice(parameters.PackingPrice);
        SetFinalPrice(parameters.FinalPrice);
        SetPackageCount(parameters.PackageCount);
        SetPackageUnitPrice(parameters.PackageUnitPrice);
        SetCheckGroup(parameters.CheckGroup);
        SetContractorId(parameters.ContractorId);
        SetDescription(parameters.Description);
        SetManagementDescription(parameters.ManagementDescription);
        SetProjectName(parameters.ProjectName);
        SetProjectCode(parameters.ProjectCode);
        SetProjectEnName(parameters.ProjectEnName);
        AddDocuments(parameters.Urls);

        AddHistory();
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetPackageId(long? value)
    {
        PackageId = value;
    }

    public void SetReferenceId(long value)
    {
        ReferenceId = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestGoodsSupply(RequestGoodsSupply value)
    {
        RequestGoodsSupply = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetSupplyType(SupplyType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetDelivaryDeadLine(DateTime? value)
    {
        DelivaryDeadLine = value;
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

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = value;
    }

    public void SetUnitPrice(decimal? value)
    {
        UnitPrice = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
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

    public void SetImportance(GoodsSupplyDetailImportance value)
    {
        Importance = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void UpdateStatus(RGSTypeStatus value, string? lastDescription)
    {
        Status = value;
        LastDescription = lastDescription;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void UpdateCheckGroup(bool value)
    {
        CheckGroup = value;
    }

    public void SetStatus(RGSTypeStatus value, string? description)
    {
        Status = value;
        if (!string.IsNullOrEmpty(description))
            SetLastDescription(description);
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            _requestGoodsSupplyTypeDocuments.ForEach(c => c.SoftDelete());

            foreach (var url in urls)
                _requestGoodsSupplyTypeDocuments.Add(RequestGoodsSupplyTypeDocument.Create(url, this));
        }
        else
            _requestGoodsSupplyTypeDocuments.ForEach(c => c.SoftDelete());
    }

    public void AddHistory()
    {
        _requestGoodsSupplyTypeHistories.Add(new RequestGoodsSupplyTypeHistory(this));
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<RequestGoodsSupplyTypeDetail> _requestGoodsSupplyTypeDetails;
    public IReadOnlyList<RequestGoodsSupplyTypeDetail> RequestGoodsSupplyTypeDetails => _requestGoodsSupplyTypeDetails;

    public IReadOnlyList<RequestGoodsSupplyTypeHistory> RequestGoodsSupplyTypeHistories => _requestGoodsSupplyTypeHistories;
    private List<RequestGoodsSupplyTypeHistory> _requestGoodsSupplyTypeHistories;

    public IReadOnlyList<RequestGoodsSupplyTypeDocument> RequestGoodsSupplyTypeDocuments => _requestGoodsSupplyTypeDocuments;
    private List<RequestGoodsSupplyTypeDocument> _requestGoodsSupplyTypeDocuments;

    public IReadOnlyList<RequestGoodsSupplyManagement> RequestGoodsSupplyManagements => _requestGoodsSupplyManagements;
    private List<RequestGoodsSupplyManagement> _requestGoodsSupplyManagements;

    private RequestGoodsSupplyType()
    {
        _requestGoodsSupplyTypeDetails = [];
        _requestGoodsSupplyTypeHistories = [];
        _requestGoodsSupplyTypeDocuments = [];
        _requestGoodsSupplyManagements = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}