namespace Engineering.Application.Services.Advertisements.Contracts.SetAdsDetails;

public class SetAdsDetailsValidator : AbstractValidator<SetAdsDetailsRequest>
{
    public SetAdsDetailsValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}