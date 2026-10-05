using Engineering.Domain.Entities.Advertisements;

namespace Engineering.Application.Services.Advertisements.Commands.CreateAdvertisement;

public record CreateAdvertisementCommand(string TitleFa,
    string TitleEn,
    string DescriptionFa,
    string DescriptionEn,
    string TechnicalCode,
    List<string>? DocumentUrls,
    bool IsActive) : ICommand<Advertisement?>;