namespace Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;

public record GetOperationInfoDependencyByIdRequest(
    long Id
     ) : IHttpRequest;
