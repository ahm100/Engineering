namespace Engineering.Application.Services.OperationInfoServices.Models.DeleteOperationInfoService;

public record DeleteOperationInfoServiceRequest(
    long OperationInfoId,
    long ServiceInfoId
     ) : IHttpRequest;
