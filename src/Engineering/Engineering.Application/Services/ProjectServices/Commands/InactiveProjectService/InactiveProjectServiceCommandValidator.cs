namespace Engineering.Application.Services.ProjectServices.Commands.InactiveProjectService;

public class InactiveProjectServiceCommandValidator : AbstractValidator<InactiveProjectServiceCommand>
{
    public InactiveProjectServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}