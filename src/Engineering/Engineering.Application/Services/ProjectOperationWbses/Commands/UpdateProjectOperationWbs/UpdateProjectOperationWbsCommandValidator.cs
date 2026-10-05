namespace Engineering.Application.Services.ProjectOperationWbses.Commands.UpdateProjectOperationWbs;

public class UpdateProjectOperationWbsCommandValidator : AbstractValidator<UpdateProjectOperationWbsCommand>
{
    public UpdateProjectOperationWbsCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
