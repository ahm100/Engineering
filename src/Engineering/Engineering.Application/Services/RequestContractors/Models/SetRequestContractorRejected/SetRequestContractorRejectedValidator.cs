namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorRejected;

public class SetRequestContractorRejectedValidator : AbstractValidator<SetRequestContractorRejectedRequest>
{
    public SetRequestContractorRejectedValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithMessage(RequestContractorErrors.InValidRequestContractor);
    }
}
