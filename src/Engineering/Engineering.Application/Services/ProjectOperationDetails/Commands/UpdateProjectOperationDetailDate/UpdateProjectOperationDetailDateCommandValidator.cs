namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetailDate;

public class UpdateProjectOperationDetailDateCommandValidator : AbstractValidator<UpdateProjectOperationDetailDateCommand>
{
    public UpdateProjectOperationDetailDateCommandValidator()
    {
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(ProjectOperationDetailErrors.InValidStartDate);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(ProjectOperationDetailErrors.InValidEndDate);
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
