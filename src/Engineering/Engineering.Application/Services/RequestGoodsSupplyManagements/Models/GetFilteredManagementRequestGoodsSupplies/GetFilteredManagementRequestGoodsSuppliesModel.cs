using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;

public record GetFilteredManagementRequestGoodsSuppliesModel()
{
    public long Id { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public string? Created { get; set; } = string.Empty;
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDisplayName => Status.GetEnumDescription();
    public GoodsSupplyType Type { get; set; }
    public string TypeDisplayName => Type.GetEnumDescription();
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDisplayName => Importance?.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? ContractorFullName { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public decimal Percent { get; set; }
    public int? RejectedNumber { get; set; } = 0;
    public int? AllInStock { get; set; } = 0;
    public int? InStockNumber { get; set; } = 0;
    public int? AllBetweenStock { get; set; } = 0;
    public int? BetweenStockNumber { get; set; } = 0;
    public int? AllCommerce { get; set; } = 0;
    public int? CommerceNuber { get; set; } = 0;
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}

public record ProductRequestReviewerModel()
{
    public decimal Percent { get; set; }
    public int? RejectedNumber { get; set; } = 0;
    public int? AllInStock { get; set; } = 0;
    public int? InStockNumber { get; set; } = 0;
    public int? AllBetweenStock { get; set; } = 0;
    public int? BetweenStockNumber { get; set; } = 0;
    public int? AllCommerce { get; set; } = 0;
    public int? CommerceNuber { get; set; } = 0;
}

