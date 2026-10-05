namespace Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;

public record UpdateAdvertisementRequest(
    long Id,
    string TitleFa,
    string TitleEn,
    string DescriptionFa,
    string DescriptionEn,
    string TechnicalCode,
    List<string>? DocumentUrls,
    bool? IsActive) : IHttpRequest;