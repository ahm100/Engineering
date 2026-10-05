using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;

public record GetReferenceTypeHistoryResponse(
    List<GetReferenceTypeHistoryModel> Data,
    int RowCount);

public record GetReferenceTypeHistoryModel
{
    public long? Id { get; set; }
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public long? ReferenceId { get; set; }
    public string? ReferenceName { get; set; }
    public string? ReferenceNameEn { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectEnName { get; set; }
    public SupplyType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public decimal? RequestedCount { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public string? DelivaryDeadLineShamsi => DelivaryDeadLine.ToShamsi();
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public decimal? FinalPrice { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? PackageId { get; set; }
    public string? Package { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public string? LastDescription { get; set; }
    public bool? IsReExamination { get; set; } = false;
    public bool? IsClosed { get; set; } = false;
    public RGSProjectModel? ProjectModel { get; set; }
}

public record RGSProjectModel
{
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectNameEn { get; set; }
}

public record GetReferenceTypeHistoryPdfModel
{
    public string? Index { get; set; }
    public string? Importance { get; set; }
    public string? Type { get; set; }
    public string? RequestedCount { get; set; }
    public string? DeliveryDeadline { get; set; }
    public string? ProjectName { get; set; }
    public string? Creator { get; set; }
    public string? Created { get; set; }
}