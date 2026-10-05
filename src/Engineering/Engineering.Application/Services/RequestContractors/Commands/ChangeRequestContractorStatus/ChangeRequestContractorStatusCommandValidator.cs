namespace Engineering.Application.Services.RequestContractors.Commands.ChangeRequestContractorStatus;

public class ChangeRequestContractorStatusCommandValidator : AbstractValidator<ChangeRequestContractorStatusCommand>
{
    public ChangeRequestContractorStatusCommandValidator()
    {
        RuleFor(oo => oo.Entity).NotNull().WithError(RequestContractorErrors.InValidRequestContractor);
        RuleFor(oo => oo.Status).IsInEnum().WithError(RequestContractorErrors.UnvalidStatus);
    }
}
