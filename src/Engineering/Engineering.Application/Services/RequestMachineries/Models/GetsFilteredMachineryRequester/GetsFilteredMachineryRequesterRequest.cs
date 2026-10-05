namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryRequester;

public record GetsFilteredMachineryRequesterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
