using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailForScheduling;

public record GetsProjectOperationDetailForSchedulingResponseModel(
    long Id,
    long ProjectOperationId,
    long MeasurementId,
    string? MeasurementName,
    string? StartDate,
    string? EndDate,
    ProjectOperationDetailStatus? Status,
    string? StatusDescription,
    int Priority,
    int Day,
    int Hour,
    string? Description
    );
