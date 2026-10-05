namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryResended;

public class SetRequestMachineryResendedValidator : AbstractValidator<SetRequestMachineryResendedRequest>
{
    public SetRequestMachineryResendedValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
