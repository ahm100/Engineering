namespace Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailStatusChanger;

public class ProjectOperationDetailStatusChangerCommandValidator : AbstractValidator<ProjectOperationDetailStatusChangerCommand>
{
    public ProjectOperationDetailStatusChangerCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).IsInEnum().WithError(ProjectOperationDetailErrors.NotValidateType);
    }
}
