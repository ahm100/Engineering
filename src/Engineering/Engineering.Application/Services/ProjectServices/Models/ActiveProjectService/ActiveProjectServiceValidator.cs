namespace Engineering.Application.Services.ProjectServices.Models.ActiveProjectService;

public class ActiveProjectServiceValidator : AbstractValidator<ActiveProjectServiceRequest>
{
    public ActiveProjectServiceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
