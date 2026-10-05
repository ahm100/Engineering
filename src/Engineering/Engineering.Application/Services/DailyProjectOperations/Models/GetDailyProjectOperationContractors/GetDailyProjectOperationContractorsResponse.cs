namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationContractors;

public record GetDailyProjectOperationContractorsResponse(
    List<GetDailyProjectOperationContractorsResponseModel> Data,
    int RowCount
    );
