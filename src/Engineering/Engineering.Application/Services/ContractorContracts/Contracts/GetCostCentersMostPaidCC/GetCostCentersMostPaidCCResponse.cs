namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;

public record GetCostCentersMostPaidCCResponse(
    List<GetCostCentersMostPaidCCModel> Data,
    int Count
    );

public class GetCostCentersMostPaidCCModel
{
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long ContractorContractHeaderId { get; set; }
    public string ContractorContractCode => ContractorContractHeaderId.ToString();
    public decimal ContractorContractAmount { get; set; }
}