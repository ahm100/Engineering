
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;

public record GetsRequestedServiceContractResponse
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public List<GetsRequestedServiceContractModel> Details { get; set; } = new();
    public int RowCount { get; set; }
}
