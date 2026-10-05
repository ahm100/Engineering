namespace Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
public record GetProjectDocHistoriesResponse(
    List<GetProjectDocHistoriesModel> Data,
    int RowCount);