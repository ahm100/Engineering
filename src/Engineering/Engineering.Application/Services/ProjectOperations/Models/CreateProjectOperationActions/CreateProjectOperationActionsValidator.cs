namespace Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperationActions;

public class CreateProjectOperationActionsValidator : AbstractValidator<CreateProjectOperationActionsRequest>
{
    public CreateProjectOperationActionsValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(OperationInfoCmts.Id);
    }
}