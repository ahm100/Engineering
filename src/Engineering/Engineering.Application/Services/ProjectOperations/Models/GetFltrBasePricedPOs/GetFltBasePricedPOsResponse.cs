using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;


public record GetFltrBasePricedPOsResponse(
    List<GetFltrBasePricedPOsModel> Data,
    List<GetFltrBasePricedPOsGroupedModel> OtherData,
    int RowCount);

public record GetFltrBasePricedPOsModel
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
    public List<GetProjectsCategoryModel>? Category { get; set; }
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Workload { get; set; }
    public decimal IncreaseRate { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ChangePrice { get; set; }
    public decimal FinalPrice => ChangePrice * Workload * IncreaseRate;
    public decimal BaseTotalPrice => BasePrice * Workload;
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? Actions => ActionData.Any() ? ActionData.Select(x => x.Value).JoinList() : "";
    public List<GetFltrBasePricedPOActionModel>? ActionData { get; set; } = new();
    public List<GetBasePricedPOActionModel>? POActionData { get; set; } = new();
}

public record GetFltrBasePricedPOsGroupedModel
{
    public long? Id { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public string? BranchName { get; set; } = string.Empty;
    public string? CategoryName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public decimal BaseTotalPrice { get; set; }
}

public record GetFltrBasePricedPOActionModel
{
    public long Id { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Value => $"{ActionName} {Price}";
}
public record GetBasePricedPOActionModel
{
    public long Id { get; set; }
    public long OIActionId { get; set; }
    public long ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public string Value => $"{ActionName} {Price}";
}