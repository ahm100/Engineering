namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryBackToOnProject;

public class SetRequestMachineryBackToOnProjectValidator : AbstractValidator<SetRequestMachineryBackToOnProjectRequest>
{
    public SetRequestMachineryBackToOnProjectValidator()
    {
        RuleForEach(oo => oo.Ids).NotNull().WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
