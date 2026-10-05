namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryRejected;

public class SetRequestMachineryRejectedValidator : AbstractValidator<SetRequestMachineryRejectedRequest>
{
    public SetRequestMachineryRejectedValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
