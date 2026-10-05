namespace Engineering.Application.Services.ProjectServices.Commands.ActiveProjectService;

public class ActiveProjectServiceCommandValidator : AbstractValidator<ActiveProjectServiceCommand>
{
    public ActiveProjectServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}