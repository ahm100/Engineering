namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdatesProjectOperationDetailDate;

public class UpdatesProjectOperationDetailDateCommandValidator : AbstractValidator<UpdatesProjectOperationDetailDateCommand>
{
    public UpdatesProjectOperationDetailDateCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotEmpty().WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
