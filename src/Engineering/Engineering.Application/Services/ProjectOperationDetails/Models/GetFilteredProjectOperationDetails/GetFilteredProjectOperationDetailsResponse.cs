using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;

public record GetFilteredProjectOperationDetailsResponse(
    List<GetFilteredProjectOperationDetailsOperationModel> Data,
    int RowCount);

public record GetFilteredProjectOperationDetailsOperationModel
{
    public long Id { get; set; }
    public decimal Length { get; set; }
    public bool LengthChangeable { get; set; }
    public decimal Width { get; set; }
    public bool WidthChangeable { get; set; }
    public decimal Height { get; set; }
    public bool HeightChangeable { get; set; }
    public decimal Weight { get; set; }
    public bool WeightChangeable { get; set; }
    public decimal Number { get; set; }
    public bool NumberChangeable { get; set; }
    public int Priority { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public long? CreatedProductId { get; set; }
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public string? CategoryId => Category.Select(x => x.CategoryId.ToString()).JoinList();
    public string? CategoryName => Category.Select(x => x.CategoryName).JoinList();
    public decimal FinalAmount { get; set; }
    public string? Description { get; set; }
    public long? CompanyId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public GetFilteredProjectOperationsOperationModel POperationModel { get; set; }
    public List<GetProjectsCategoryModel>? Category { get; set; }
}

public record GetFilteredProjectOperationsOperationModel
{
    public long Id { get; set; }
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Workload { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ChangePrice { get; set; }
    public decimal FinalPrice => ChangePrice * Workload;
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
