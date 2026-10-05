namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;

public record GetContractGuaranteesResponse(long ContractId, List<GetContractGuaranteesModel> Items);
