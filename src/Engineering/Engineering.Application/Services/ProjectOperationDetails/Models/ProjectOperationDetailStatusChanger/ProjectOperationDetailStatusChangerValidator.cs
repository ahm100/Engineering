
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public class ProjectOperationDetailStatusChangerValidator : AbstractValidator<ProjectOperationDetailStatusChangerRequest>
{
    public ProjectOperationDetailStatusChangerValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).IsInEnum().WithError(ProjectOperationDetailErrors.NotValidateType);
    }
}
