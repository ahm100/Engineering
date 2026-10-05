namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorConfirmed;

public class SetRequestContractorConfirmedValidator : AbstractValidator<SetRequestContractorConfirmedRequest>
{
    public SetRequestContractorConfirmedValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithError(RequestContractorErrors.InValidRequestContractor);
    }
}
