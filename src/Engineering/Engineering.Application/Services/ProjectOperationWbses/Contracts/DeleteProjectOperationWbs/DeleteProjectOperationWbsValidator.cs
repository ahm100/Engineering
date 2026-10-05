namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;

public class DeleteProjectOperationWbsValidator : AbstractValidator<DeleteProjectOperationWbsRequest>
{
    public DeleteProjectOperationWbsValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
