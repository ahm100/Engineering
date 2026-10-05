namespace Engineering.Application.Services.Advertisements.Contracts.SetAdsDetails;

public record SetAdsDetailsRequest(
    long Id,
    string? AdEnName,
    string? DescriptionFa,
    string? DescriptionEn
    ) : IHttpRequest;