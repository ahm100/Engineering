using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;

public record GetRGSTypeByRGSIdResponse(
    List<GetRGSTypeByRGSIdModel> Data,
    int RowCount);

public record GetRGSTypeByRGSIdModel
{
    public long? Id { get; set; }
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public long? ReferenceId { get; set; }
    public string? ReferenceName { get; set; }
    public string? ReferenceNameEn { get; set; }
    public string? ReferenceCode { get; set; }
    public string? Measure { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectEnName { get; set; }
    public SupplyType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public RGSTypeStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
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
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public string? LastDescription { get; set; }
    public ResProjectTypeModel? ProjectTypeModel { get; set; }
    public List<GetDetailByRGSTypeIdModel>? Details { get; set; }
}

public record ResProjectTypeModel
{
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectEnName { get; set; }
}