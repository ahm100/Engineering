namespace Engineering.Application.Services.Advertisements.Commands.SetAdsDetails;

public class SetAdsDetailsCommandValidator : AbstractValidator<SetAdsDetailsCommand>
{
    public SetAdsDetailsCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}