namespace Engineering.Application.Services.Branchs.Commands.InactiveBranch;

public class InactiveBranchCommandValidator : AbstractValidator<InactiveBranchCommand>
{
    public InactiveBranchCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(BranchErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}