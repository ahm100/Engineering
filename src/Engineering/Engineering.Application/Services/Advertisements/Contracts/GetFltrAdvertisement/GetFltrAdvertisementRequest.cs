namespace Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;

public record GetFltrAdvertisementRequest(string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize) : IHttpRequest;