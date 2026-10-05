
namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoProjectOperationsValidator;

public record GetOperationInfoProjectOperationsValidatorQuery(
    long ProjectId,
    long OperationInfoId,
    long? EmployerContractId,
    long UnitOfMeasurementId
    ) : IQuery<bool>;