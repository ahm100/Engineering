namespace Engineering.Application.Services.ProjectServices.Commands.CreateProjectService;

public class CreateProjectServiceCommandValidator : AbstractValidator<CreateProjectServiceCommand>
{
    public CreateProjectServiceCommandValidator()
    {
        RuleFor(oo => oo.Project).NotNull().WithError(ProjectServiceErrors.ProjectIsEmpty);
        RuleFor(oo => oo.ServiceInfo).NotNull().WithError(ProjectServiceErrors.ServiceIsEmpty);
        RuleFor(oo => oo.ContractorId).NotNull().WithError(ProjectServiceErrors.ContractorIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ProjectServiceErrors.IsActiveIsEmpty);
    }
}