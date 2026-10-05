namespace Engineering.Application.Services.ProjectServices.Commands.DeleteProjectServiceDetail;

public class DeleteProjectServiceDetailCommandValidator : AbstractValidator<DeleteProjectServiceDetailCommand>
{
    public DeleteProjectServiceDetailCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
