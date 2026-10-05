namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryPending;

public class SetRequestMachineryPendingValidator : AbstractValidator<SetRequestMachineryPendingRequest>
{
    public SetRequestMachineryPendingValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
