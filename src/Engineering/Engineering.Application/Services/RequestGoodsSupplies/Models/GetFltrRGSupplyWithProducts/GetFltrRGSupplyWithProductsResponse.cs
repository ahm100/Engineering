using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;

public record GetFltrRGSupplyWithProductsResponse
(
    List<GetFltrRGSupplyWithProductsModel> Data,
    int RowCount
);
public class GetFltrRGSupplyWithProductsModel
{
    public long Id { get; set; }
    public string? RequestNumber { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? MeasureId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public GoodsSupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? RequestedDate { get; set; }
    public string? RequestedDateShamsi => TimeCalculator.ConvertToShamsi(RequestedDate);
    public DateTime CreatedOn { get; set; }
    public string? CreatedOnShamsi => TimeCalculator.ConvertToShamsi(CreatedOn);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public GoodsSupplyDetailImportance? MaxImportance { get; set; }
    public bool? IsProjectSupply { get; set; }
    public bool? IsPettyCash { get; set; }
    public DateTime? DeliveryDeadline { get; set; }
    public string? DeliveryDeadlineShamsi => DeliveryDeadline.ToShamsi();
    public string? RegistrationNumber { get; set; }
    public long? RequestingOrganizationId { get; set; }
    public string? RequestingOrganization { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string? ConsumptionAddress { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public string? PurchaseLocationDescription => PurchaseLocation?.GetEnumDescription();
    public PurchaseReason? PurchaseReason { get; set; }
    public string? PurchaseReasonDescription => PurchaseReason?.GetEnumDescription();
    public string? MaxImportanceDescription => MaxImportance is null ? null : MaxImportance.GetEnumDescription();
};