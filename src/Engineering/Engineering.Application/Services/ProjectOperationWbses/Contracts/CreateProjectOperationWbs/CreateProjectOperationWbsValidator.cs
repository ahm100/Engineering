namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.CreateProjectOperationWbs;

public class CreateProjectOperationWbsValidator : AbstractValidator<CreateProjectOperationWbsRequest>
{
    public CreateProjectOperationWbsValidator()
    {
        RuleFor(oo => oo.ProjectWbsId)
            .IsPositive(WbsCmts.ProjectWbs);
        RuleForEach(oo => oo.ProjectOperationIds)
            .IsPositive(GlobalCmts.ProjectOperation);
    }
}