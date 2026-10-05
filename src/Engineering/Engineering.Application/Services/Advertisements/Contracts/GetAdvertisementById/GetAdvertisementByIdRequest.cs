namespace Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;

public record GetAdvertisementByIdRequest(
    long Id) : IHttpRequest;