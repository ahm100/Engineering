namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryManagerConfirm;

public class SetRequestMachineryManagerConfirmValidator : AbstractValidator<SetRequestMachineryManagerConfirmRequest>
{
    public SetRequestMachineryManagerConfirmValidator()
    {
        RuleForEach(oo => oo.Ids).NotNull().WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
