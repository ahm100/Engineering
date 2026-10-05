namespace Engineering.Application.Services.Projects.Models.Delete;

public class DeleteProjectValidator : AbstractValidator<DeleteProjectRequest>
{
    public DeleteProjectValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectErrors.IdIsEmpty);
    }
}
