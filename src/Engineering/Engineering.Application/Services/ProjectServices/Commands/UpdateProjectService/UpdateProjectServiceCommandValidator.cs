namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectService;

public class UpdateProjectServiceCommandValidator : AbstractValidator<UpdateProjectServiceCommand>
{
    public UpdateProjectServiceCommandValidator()
    {
        RuleFor(oo => oo.ProjectService)
            .NotNull().NotEmpty().WithError(ProjectServiceErrors.ProjectServiceIsEmpty);

        RuleFor(oo => oo.ServiceInfo)
            .NotNull().WithError(ProjectServiceErrors.ServiceIsEmpty);

        RuleFor(oo => oo.ContractorId)
            .NotNull().WithError(ProjectServiceErrors.ContractorIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.IsActive)
            .NotNull().WithError(ProjectServiceErrors.IsActiveIsEmpty);
    }
}