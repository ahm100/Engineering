namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;

public record GetFilteredContractorContractsByContractorModel
{
    public long Id { get; set; }
    public long? ContractorNumber { get; set; }
    public string? CostCenterName { get; set; }
    public string? ProjectName { get; set; }
    public long ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
