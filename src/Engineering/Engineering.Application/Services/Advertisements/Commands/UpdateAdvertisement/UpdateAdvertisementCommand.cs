using Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;

namespace Engineering.Application.Services.Advertisements.Commands.UpdateAdvertisement;

public record UpdateAdvertisementCommand(long Id,
    string TitleFa,
    string TitleEn,
    string DescriptionFa,
    string DescriptionEn,
    string TechnicalCode,
    List<string>? DocumentUrls,
    bool? IsActive) : ICommand<UpdateAdvertisementResponse?>;