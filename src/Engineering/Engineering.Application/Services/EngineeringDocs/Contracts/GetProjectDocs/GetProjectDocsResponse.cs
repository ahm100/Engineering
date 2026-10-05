namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;

public record GetProjectDocsResponse(
    List<GetProjectDocsModel> Data,
    int RowCount);