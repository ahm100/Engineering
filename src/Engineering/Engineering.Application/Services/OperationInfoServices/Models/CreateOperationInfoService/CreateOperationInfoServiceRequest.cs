using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;

public record CreateOperationInfoServiceRequest(
    List<long> OperationInfoIds,
    List<CreateOperationInfoServiceRequestModel>? ServiceInfos
     ) : IHttpRequest;

public record CreateOperationInfoServiceModelRequest(
    List<long>? OperationInfoIds,
    List<OperationInfo>? OperationInfos,
    List<CreateOperationInfoServiceRequestModel>? ServiceInfos
     ) : IHttpRequest;
