namespace Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;

public record GetProjectDocHistoriesRequest(
    long ProjectDocId,
    int PageIndex,
    int PageSize
) : IHttpRequest;