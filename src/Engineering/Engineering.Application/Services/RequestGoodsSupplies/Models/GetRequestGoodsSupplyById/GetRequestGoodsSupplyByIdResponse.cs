using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;

public class GetRequestGoodsSupplyByIdResponse
{
    public long Id { get; set; }
    public bool CanUpdate { get; set; } = true;
    public long? SupplyerId { get; set; }
    public string? SupplyerName { get; set; } = string.Empty;
    public long? BuyerId { get; set; }
    public string? BuyerFullName { get; set; } = string.Empty;
    public GetsRequestGoodsSupplyCurrencyModel? Currency { get; set; }
    public decimal? Workload { get; set; }
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public string RequestNumber { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectEnName { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public GoodsSupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? RequestedDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public decimal? TransferPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public long? MeasureId { get; set; }
    public long? RequestingOrganizationId { get; set; }
    public string? RequestingOrganization { get; set; }
    public string? RequestingOrganizationEn { get; set; }
    public string? ConsumptionAddress { get; set; }
    public bool? IsPettyCash { get; set; }
    public bool IsProjectSupply { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public string? PurchaseLocationDescription => PurchaseLocation?.GetEnumDescription();
    public PurchaseReason? PurchaseReason { get; set; }
    public string? PurchaseReasonDescription => PurchaseReason?.GetEnumDescription();
    public string? MeasureName { get; set; } = string.Empty;
    public List<GetsRequestGoodsSupplyDetailModel>? Details { get; set; } = new();
}

public record GetsRequestGoodsSupplyCurrencyModel(
    long? Id,
    string? Name
    );

public record CalculatProjectOperationDetailSupplyCounterModel
{
    public long ConsumableVolumeId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? TotalEstimatedCount { get; set; } = 0;
    public decimal? TolerancePercentage { get; set; } = 0;
    public decimal? ToleranceCount { get; set; } = 0;
    public decimal? TotalRequestedCount { get; set; } = 0;
    public decimal? TotalSupplyCount { get; set; } = 0;
    public decimal? TotalDifferenceCount { get; set; } = 0;
    public decimal? TotalRemainedCount { get; set; } = 0;
}

