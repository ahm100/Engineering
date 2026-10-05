namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;

public record GetContractRegistrationGridResponse(
    List<GetContractRegistrationGridModel> Data,
    int RowCount);
