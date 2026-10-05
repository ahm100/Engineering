namespace Engineering.Application.Services.ProjectOperationWbses.Commands.DeleteProjectOperationWbs;

public class DeleteProjectOperationWbsCommandValidator : AbstractValidator<DeleteProjectOperationWbsCommand>
{
    public DeleteProjectOperationWbsCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}