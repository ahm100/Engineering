namespace Engineering.Application.Services.Branchs.Commands.DisableBranch;

public class DisableBranchCommandValidator : AbstractValidator<DisableBranchCommand>
{
    public DisableBranchCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(BranchErrors.BranchWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}