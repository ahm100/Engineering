namespace Engineering.Application.Services.ProjectOperationDetails.Commands.DeleteProjectOperationDetail;

public class DeleteProjectOperationDetailCommandValidator : AbstractValidator<DeleteProjectOperationDetailCommand>
{
    public DeleteProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
