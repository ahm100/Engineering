namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;

public record UpdateContractStructureRequest(
    long ContractId,
    List<ContractStructureItem> Items) : IHttpRequest;