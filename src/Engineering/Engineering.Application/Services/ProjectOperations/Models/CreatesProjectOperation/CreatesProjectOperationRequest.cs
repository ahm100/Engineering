using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.CreatesProjectOperation;

public record CreatesProjectOperationRequest(
    List<CreatesProjectOperationModel> ProjectOperationModel
     ) : IHttpRequest;

public record CreatesProjectOperationModel(
    long? Id,
    long? ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    decimal BasePrice,
    decimal? ChangedPrice,
    int? Priority,
    long UnitOfMeasurementId,
    ProjectOperationStatus? ProjectOperationStatus,
    bool GoodsInProgress,
    DateTime? BaselineStartDate,
    DateTime? BaselineFinishDate,
    int? BaselineDuration,
    string? Description,
    List<string>? Urls,
    bool IsDeleted
     );
