
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;

public class GetContractorContractsDateResponse
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}

public class ContractorContractsDateModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
