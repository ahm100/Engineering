namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoContractors;

public record GetOperationInfoContractorsRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
