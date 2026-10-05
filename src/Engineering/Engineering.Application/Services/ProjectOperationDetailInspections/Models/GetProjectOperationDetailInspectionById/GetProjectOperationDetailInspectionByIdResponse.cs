using Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionDocumentModel;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetProjectOperationDetailInspectionById;

public record GetProjectOperationDetailInspectionByIdResponse
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public long? OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public long? OperationLocationId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount { get; set; }
    public DateTime? InspectionDate { get; set; }
    public string? InspectionDateShamsi => TimeCalculator.ConvertToShamsi(InspectionDate);
    public string? Description { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public string? CreateDateShamsi => TimeCalculator.ConvertToShamsi(CreateDate);
    public List<ProjectOperationDetailInspectionDocumentResponseModel>? Documents { get; set; } = new();
}
