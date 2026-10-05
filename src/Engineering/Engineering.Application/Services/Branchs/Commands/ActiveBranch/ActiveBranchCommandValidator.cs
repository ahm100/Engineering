namespace Engineering.Application.Services.Branchs.Commands.ActiveBranch;

public class ActiveBranchCommandValidator : AbstractValidator<ActiveBranchCommand>
{
    public ActiveBranchCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(BranchErrors.BranchWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}