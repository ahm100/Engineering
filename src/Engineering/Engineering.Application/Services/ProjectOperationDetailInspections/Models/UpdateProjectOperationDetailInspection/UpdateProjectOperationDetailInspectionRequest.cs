namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.UpdateProjectOperationDetailInspection;

public record UpdateProjectOperationDetailInspectionRequest(
    long Id,
    long projectId,
    long? OperationInfoId,
    long? OperationLocationId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    DateTime? InspectionDate,
    string? Description,
    List<string>? Documents) : IHttpRequest;
