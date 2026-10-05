using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperation;

public record CreateProjectOperationRequest(
    long? ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    decimal BasePrice,
    int? Priority,
    long UnitOfMeasurementId,
    ProjectOperationStatus ProjectOperationStatus,
    bool GoodsInProgress,
    DateTime? BaselineStartDate,
    DateTime? BaselineFinishDate,
    int? BaselineDuration,
    string? Description,
    List<string>? Urls
     ) : IHttpRequest;
