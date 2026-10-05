namespace Engineering.Application.Services.Advertisements.Commands.UpdateAdvertisement;

public class UpdateAdvertisementCommandValidator : AbstractValidator<UpdateAdvertisementCommand>
{
    public UpdateAdvertisementCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.TitleFa)
            .IsFullString(AdvertisementCmts.TitleFa, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.TitleEn)
            .IsFullString(AdvertisementCmts.TitleEn, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.DescriptionFa)
            .IsFullString(AdvertisementCmts.DescriptionFa, 2000, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.DescriptionEn)
            .IsFullString(AdvertisementCmts.DescriptionEn, 2000, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.TechnicalCode)
            .IsFullString(AdvertisementCmts.TechnicalCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}