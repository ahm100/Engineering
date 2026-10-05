namespace Engineering.Application.Services.ProjectOperationWbses.Commands.CreateProjectOperationWbs;

public class CreateProjectOperationWbsCommandValidator : AbstractValidator<CreateProjectOperationWbsCommand>
{
    public CreateProjectOperationWbsCommandValidator()
    {
        RuleFor(oo => oo.ProjectWbsId)
            .IsPositive(WbsCmts.ProjectWbs);
        RuleForEach(oo => oo.ProjectOperationIds)
            .IsPositive(GlobalCmts.ProjectOperation);
    }
}