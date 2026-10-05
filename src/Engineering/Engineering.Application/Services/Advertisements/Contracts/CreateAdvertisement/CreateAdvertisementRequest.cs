namespace Engineering.Application.Services.Advertisements.Contracts.CreateAdvertisement;

public record CreateAdvertisementRequest(string TitleFa,
    string TitleEn,
    string DescriptionFa,
    string DescriptionEn,
    string TechnicalCode,
    List<string>? DocumentUrls,
    bool IsActive) : IHttpRequest;