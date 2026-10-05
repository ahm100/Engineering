namespace Engineering.Application.Services.ProjectOperations.Queries.GetExistingOperationInfoCodesInProject;

public record GetExistingOperationInfoCodesInProjectQuery(
    long ProjectId,
    List<string> OperationInfoCodes
) : IQuery<List<string>>;