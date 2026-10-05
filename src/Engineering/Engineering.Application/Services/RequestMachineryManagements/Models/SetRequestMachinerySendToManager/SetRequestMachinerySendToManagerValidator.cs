namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachinerySendToManager;

public class SetRequestMachinerySendToManagerValidator : AbstractValidator<SetRequestMachinerySendToManagerRequest>
{
    public SetRequestMachinerySendToManagerValidator()
    {
        RuleForEach(oo => oo.Ids).NotNull().WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
