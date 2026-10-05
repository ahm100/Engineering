namespace Engineering.Application.Services.ProjectOperationDetails.Commands.SetProjectOperationDetailPriority;

public class SetProjectOperationDetailPriorityCommandValidator : AbstractValidator<SetProjectOperationDetailPriorityCommand>
{
    public SetProjectOperationDetailPriorityCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(ProjectOperationDetailErrors.PriorityIsEmpty);
    }
}
