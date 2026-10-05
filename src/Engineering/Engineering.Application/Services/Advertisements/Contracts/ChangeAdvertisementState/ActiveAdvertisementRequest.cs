namespace Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;

public record ActiveAdvertisementRequest(
    long Id
    ) : IHttpRequest;
