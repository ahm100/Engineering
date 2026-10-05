
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelExporter;

public record GetsSupplyManagementExcelExporterResponseModel
{
    public long Id { get; set; }
    public string? RequestNumber { get; set; } = string.Empty;
    public string? TypeDescription { get; set; } = string.Empty;
    public string? MaxImportanceDescription { get; set; } = string.Empty;
    public string? StatusDescription { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public string? CreatedOn { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public decimal Percent { get; set; }
    public int? RejectedNumber { get; set; } = 0;
    public int? AllInStock { get; set; } = 0;
    public int? InStockNumber { get; set; } = 0;
    public int? AllBetweenStock { get; set; } = 0;
    public int? BetweenStockNumber { get; set; } = 0;
    public int? AllCommerce { get; set; } = 0;
    public int? CommerceNuber { get; set; } = 0;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
