namespace Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;

public record DeleteAdvertisementRequest(
    long Id) : IHttpRequest;