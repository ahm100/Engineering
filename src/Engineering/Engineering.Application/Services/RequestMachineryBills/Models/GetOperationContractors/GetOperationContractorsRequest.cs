namespace Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;

public record GetOperationContractorsRequest(
    long? RequestMachineryId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
