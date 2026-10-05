namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryDone;

public class SetRequestMachineryDoneValidator : AbstractValidator<SetRequestMachineryDoneRequest>
{
    public SetRequestMachineryDoneValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
