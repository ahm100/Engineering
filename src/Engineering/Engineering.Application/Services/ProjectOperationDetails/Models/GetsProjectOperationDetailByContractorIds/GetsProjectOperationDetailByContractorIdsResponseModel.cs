namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;

public record GetsProjectOperationDetailByContractorIdsResponseModel
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
    public List<ProjectOperationDetailResponseModel>? projectOperationDetails { get; set; }
};

public record ProjectOperationDetailResponseModel
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ProjectOperationId { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; }
    public string? PrivateCode { get; set; }
    public string? PublicName { get; set; }
    public string? PublicCode { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal DoneFinalAmount { get; set; }
    public decimal RemaindedFinalAmount => FinalAmount - DoneFinalAmount;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Description { get; set; } = string.Empty;
}