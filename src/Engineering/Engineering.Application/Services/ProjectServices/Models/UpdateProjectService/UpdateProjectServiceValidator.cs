namespace Engineering.Application.Services.ProjectServices.Models.UpdateProjectService;

public class UpdateProjectServiceValidator : AbstractValidator<UpdateProjectServiceRequest>
{
    public UpdateProjectServiceValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ContractorId)
            .NotNull().WithError(ProjectServiceErrors.ContractorIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ServiceInfoId)
            .NotNull().WithError(ProjectServiceErrors.ServiceIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
