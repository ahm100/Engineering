namespace Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperationActions;

public class DeleteProjectOperationActionsValidator : AbstractValidator<DeleteProjectOperationActionsRequest>
{
    public DeleteProjectOperationActionsValidator()
    {
        RuleForEach(oo => oo.ProjectOperationActionIds)
            .IsPositive(OperationInfoCmts.Id);
    }
}