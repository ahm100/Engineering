namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.UpdateProjectOperationWbs;

public class UpdateProjectOperationWbsValidator : AbstractValidator<UpdateProjectOperationWbsRequest>
{
    public UpdateProjectOperationWbsValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
