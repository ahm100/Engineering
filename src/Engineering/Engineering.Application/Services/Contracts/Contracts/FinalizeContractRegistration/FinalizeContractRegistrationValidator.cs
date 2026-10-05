namespace Engineering.Application.Services.Contracts.Contracts.FinalizeContractRegistration;

public class FinalizeContractRegistrationValidator : AbstractValidator<FinalizeContractRegistrationRequest>
{
    public FinalizeContractRegistrationValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
