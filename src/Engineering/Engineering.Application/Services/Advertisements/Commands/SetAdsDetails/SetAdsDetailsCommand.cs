using Engineering.Application.Services.Advertisements.Contracts.SetAdsDetails;

namespace Engineering.Application.Services.Advertisements.Commands.SetAdsDetails;

public record SetAdsDetailsCommand(
    long Id,
    string? AdEnName,
    string? DescriptionFa,
    string? DescriptionEn
    ) : ICommand<SetAdsDetailsResponse?>;