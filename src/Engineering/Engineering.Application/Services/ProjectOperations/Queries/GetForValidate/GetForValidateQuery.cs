using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetForValidate;

public record GetProjectOperationForValidateQuery(
    long ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    long UnitOfMeasurementId
    ) : IQuery<ProjectOperation>;