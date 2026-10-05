using Engineering.Application.Services.DailyProjectOperations.Commands.UpdateProjectOperationStatus;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateProjectOperationDetailStatus;

public class UpdateProjectOperationDetailStatusCommandValidator : AbstractValidator<UpdateProjectOperationDetailStatusCommand>
{
    public UpdateProjectOperationDetailStatusCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
        RuleFor(oo => oo.Status).IsInEnum().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailStatus);
    }
}
