namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOnProject;

public class SetRequestMachineryOnProjectValidator : AbstractValidator<SetRequestMachineryOnProjectRequest>
{
    public SetRequestMachineryOnProjectValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
