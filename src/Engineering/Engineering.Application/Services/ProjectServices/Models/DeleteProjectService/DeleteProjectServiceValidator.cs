namespace Engineering.Application.Services.ProjectServices.Models.DeleteProjectService;

public class DeleteProjectServiceValidator : AbstractValidator<DeleteProjectServiceRequest>
{
    public DeleteProjectServiceValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
