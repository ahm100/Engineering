namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorPending;

public class SetRequestContractorPendingValidator : AbstractValidator<SetRequestContractorPendingRequest>
{
    public SetRequestContractorPendingValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithError(RequestContractorErrors.InValidRequestContractor);
    }
}
