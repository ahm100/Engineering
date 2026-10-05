namespace Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;

public record InActiveAdvertisementRequest(
    long Id
    ) : IHttpRequest;
