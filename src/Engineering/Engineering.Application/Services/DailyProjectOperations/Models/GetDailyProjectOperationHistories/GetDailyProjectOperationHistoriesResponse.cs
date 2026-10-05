namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;

public record GetDailyProjectOperationHistoriesResponse(
    GetTotalDailyProjectOperationHistoriesDetailModel OtherData,
    List<GetDailyProjectOperationHistoriesDetailModel> Data,
    int RowCount
    );