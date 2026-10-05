namespace Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;

public record GetFilteredContractsResponse(
    List<GetFilteredContractsModel> Data,
    int RowCount);