namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;

public record GetCostCentersMostRecentCCResponse(
    List<GetCostCentersMostRecentCCModel> Data,
    int Count
    );

public class GetCostCentersMostRecentCCModel
{
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long ContractorContractHeaderId { get; set; }
    public string ContractorContractCode => ContractorContractHeaderId.ToString();
    public decimal ContractorContractAmount { get; set; }
}