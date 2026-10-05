namespace Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;

public record GetOperationContractorsResponse(
    List<GetOperationContractorsResponseModel> Data,
    int RowCount
    );
