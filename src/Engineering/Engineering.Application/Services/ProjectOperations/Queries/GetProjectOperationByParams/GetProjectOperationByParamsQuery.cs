using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByParams;

public record GetProjectOperationByParamsQuery(
    long ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    long MeasurementId
    ) : IQuery<ProjectOperation>;