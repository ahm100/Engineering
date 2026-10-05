
namespace Engineering.Application.Services.TransportationRequests.Models.GetsDailyCreator;

public record GetsDailyCreatorResponse(
    List<GetsDailyCreatorResponseModel> Data,
    int RowCount
    );
