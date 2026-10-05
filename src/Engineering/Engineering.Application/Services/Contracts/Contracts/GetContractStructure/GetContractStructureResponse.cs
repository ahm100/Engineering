namespace Engineering.Application.Services.Contracts.Contracts.GetContractStructure;

public record GetContractStructureResponse(
    long ContractId,
    List<GetContractStructureItemModel> Items);