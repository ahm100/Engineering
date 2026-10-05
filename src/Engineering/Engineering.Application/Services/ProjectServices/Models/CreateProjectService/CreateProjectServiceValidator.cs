namespace Engineering.Application.Services.ProjectServices.Models.CreateProjectService;

public class CreateProjectServiceValidator : AbstractValidator<CreateProjectServiceRequest>
{
    public CreateProjectServiceValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(ProjectServiceErrors.ProjectIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ContractorId)
            .NotNull().WithError(ProjectServiceErrors.ContractorIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ServiceInfoId)
            .NotNull().WithError(ProjectServiceErrors.ServiceIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        When(oo => oo.OperationInfoServices != null, () =>
        {
            RuleForEach(oo => oo.OperationInfoServices)
                .NotNull().WithError(ProjectServiceErrors.OperationInfoServiceIsEmpty);
        });
    }
}
