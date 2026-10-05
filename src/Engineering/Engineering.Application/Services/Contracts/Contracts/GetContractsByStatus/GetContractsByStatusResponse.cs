namespace Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;

public record GetContractsByStatusResponse(
    List<GetContractsByStatusModel> Data,
    int RowCount);