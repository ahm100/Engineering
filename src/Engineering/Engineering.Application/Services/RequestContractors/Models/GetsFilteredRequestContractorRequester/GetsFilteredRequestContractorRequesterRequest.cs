namespace Engineering.Application.Services.RequestContractors.Models.GetsFilteredRequestContractorRequester;

public record GetsFilteredRequestContractorRequesterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
