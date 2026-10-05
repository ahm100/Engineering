using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Projects;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperation;

public record UpdateProjectOperationCommand(
    ProjectOperation ProjectOperation,
    Project Project,
    OperationInfo? OperationInfo,
    EmployerContract? EmployerContract,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    decimal ChangedPrice,
    int? Priority,
    long UnitOfMeasurementId,
    bool GoodsInProgress,
    string? Description,
    List<string>? Urls,
    long? CompanyId
    ) : ICommand<ProjectOperation>;