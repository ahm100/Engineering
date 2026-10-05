
namespace Engineering.Application.Services.ProjectOperationDetails.Models.SetProjectOperationDetailPriority;

public class SetProjectOperationDetailPriorityValidator : AbstractValidator<SetProjectOperationDetailPriorityRequest>
{
    public SetProjectOperationDetailPriorityValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(ProjectOperationDetailErrors.PriorityIsEmpty);
    }
}
