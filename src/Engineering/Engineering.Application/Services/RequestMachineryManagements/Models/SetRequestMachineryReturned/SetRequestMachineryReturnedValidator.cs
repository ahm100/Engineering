namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryReturned;

public class SetRequestMachineryReturnedValidator : AbstractValidator<SetRequestMachineryReturnedRequest>
{
    public SetRequestMachineryReturnedValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
