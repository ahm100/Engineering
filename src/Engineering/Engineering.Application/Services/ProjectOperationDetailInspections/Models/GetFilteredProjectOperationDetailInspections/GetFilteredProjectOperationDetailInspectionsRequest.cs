namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetFilteredProjectOperationDetailInspections;

public record GetFilteredProjectOperationDetailInspectionsRequest(
    long? CostCenterId,
    long? ProjectId,
    long? OperationInfoId,
    long? OperationLocationId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
