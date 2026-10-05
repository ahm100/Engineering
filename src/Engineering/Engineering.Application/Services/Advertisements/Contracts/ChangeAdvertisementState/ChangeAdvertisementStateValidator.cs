namespace Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;

public class ChangeAdvertisementStateValidator : AbstractValidator<ChangeAdvertisementStateRequest>
{
    public ChangeAdvertisementStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}