namespace Engineering.Application.Services.Contracts.Contracts.GetContractStructure;

public record GetContractStructureRequest(
    long ContractId) : IHttpRequest;