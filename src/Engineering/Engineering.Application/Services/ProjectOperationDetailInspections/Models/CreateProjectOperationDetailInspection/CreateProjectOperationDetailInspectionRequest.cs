namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.CreateProjectOperationDetailInspection;

public record CreateProjectOperationDetailInspectionRequest(
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
