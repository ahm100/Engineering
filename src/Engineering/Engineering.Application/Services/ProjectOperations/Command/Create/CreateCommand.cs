using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.Create;

public record CreateProjectOperationCommand(
    Project Project,
    OperationInfo OperationInfo,
    EmployerContract? EmployerContract,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    int? Priority,
    long UnitOfMeasurementId,
    ProjectOperationStatus ProjectOperationStatus,
    bool GoodsInProgress,
    DateTime? BaselineStartDate,
    DateTime? BaselineFinishDate,
    int? BaselineDuration,
    string? Description,
    List<string>? Urls,
    long? CompanyId
    ) : ICommand<ProjectOperation>;