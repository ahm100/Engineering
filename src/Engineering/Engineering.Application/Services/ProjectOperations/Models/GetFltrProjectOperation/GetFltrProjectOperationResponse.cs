namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;

public record GetFltrProjectOperationResponse(
    List<GetFltrProjectOperationModel> Data,
    List<GetFltrProjectOperationGroupedModel> OtherData,
    int RowCount);

public record GetFltrProjectOperationModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Workload { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ChangePrice { get; set; }
    public decimal FinalPrice => ChangePrice * Workload;
    public decimal BaseTotalPrice => BasePrice * Workload;
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}

public record GetFltrProjectOperationGroupedModel
{
    public long? Id { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public string? BranchName { get; set; } = string.Empty;
    public string? CategoryName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public decimal BaseTotalPrice { get; set; }
}