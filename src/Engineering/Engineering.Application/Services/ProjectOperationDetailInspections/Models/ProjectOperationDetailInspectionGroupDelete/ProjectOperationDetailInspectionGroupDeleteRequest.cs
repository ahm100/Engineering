namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionGroupDelete;

public record ProjectOperationDetailInspectionGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
