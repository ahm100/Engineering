namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationByParams;

public record GetProjectOperationByParamsRequest(
    long OperationInfoId,
    long ProjectId,
    long? EmployerContractId,
    long MeasurementId
     ) : IHttpRequest;
