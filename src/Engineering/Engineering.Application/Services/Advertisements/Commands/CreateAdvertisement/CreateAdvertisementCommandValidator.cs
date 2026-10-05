using Engineering.Application.Services.Advertisements.Contracts.CreateAdvertisement;

namespace Engineering.Application.Services.Advertisements.Commands.CreateAdvertisement;

public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementRequest>
{
    public CreateAdvertisementCommandValidator()
    {
        RuleFor(c => c.TitleFa)
            .IsFullString(AdvertisementCmts.TitleFa, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.TitleEn)
            .IsFullString(AdvertisementCmts.TitleEn, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.DescriptionFa)
            .IsFullString(AdvertisementCmts.DescriptionFa, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.DescriptionEn)
            .IsFullString(AdvertisementCmts.DescriptionEn, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.TechnicalCode)
            .IsFullString(AdvertisementCmts.TechnicalCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}