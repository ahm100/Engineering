namespace Engineering.Application.Services.Advertisements.Commands.ChangeAdvertisementState;

public class ChangeAdvertisementStateCommandValidator : AbstractValidator<ChangeAdvertisementStateCommand>
{
    public ChangeAdvertisementStateCommandValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}