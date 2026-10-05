
namespace Engineering.Application.Services.ProjectOperations.Models.GetsForDailyProjectOperations;

public record GetsForDailyProjectOperationsResponse(
    List<GetsForDailyProjectOperationsModel> Data,
    int RowCount);
