namespace Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;

public record ChangeAdvertisementStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
