using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupply;

public record GetsRequestGoodsSupplyModel
{
    public long Id { get; set; }
    public string? RequestNumber { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public GoodsSupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? RequestedDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public GoodsSupplyDetailImportance? MaxImportance { get; set; }
    public string? MaxImportanceDescription => MaxImportance is null ? null : MaxImportance.GetEnumDescription();
}

