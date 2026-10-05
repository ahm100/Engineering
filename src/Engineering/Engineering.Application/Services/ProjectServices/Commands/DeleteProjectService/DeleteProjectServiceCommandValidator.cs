namespace Engineering.Application.Services.ProjectServices.Commands.DisableProjectService;

public class DeleteProjectServiceCommandValidator : AbstractValidator<DeleteProjectServiceCommand>
{
    public DeleteProjectServiceCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}