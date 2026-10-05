using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperation;

public record UpdateProjectOperationRequest(
    long Id,
    long? ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    decimal ChangedPrice,
    int? Priority,
    long UnitOfMeasurementId,
    ProjectOperationStatus Status,
    bool GoodsInProgress,
    string? Description,
    List<string>? Urls
     ) : IHttpRequest;
