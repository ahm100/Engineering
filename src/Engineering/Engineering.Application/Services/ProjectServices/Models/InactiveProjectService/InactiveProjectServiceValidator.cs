namespace Engineering.Application.Services.ProjectServices.Models.InactiveProjectService;

public class InactiveProjectServiceValidator : AbstractValidator<InactiveProjectServiceRequest>
{
    public InactiveProjectServiceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
