namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;

public record GetFltrProjectContractorsResponse(
    List<GetFltrProjectContractorsModel> Data,
    int RowCount
    );

public record GetFltrProjectContractorsModel
{
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string ContractorName { get; set; } = string.Empty;
}