namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationProgress;

public class GetProjectOperationProgressQueryValidator : AbstractValidator<GetProjectOperationProgressQuery>
{
    public GetProjectOperationProgressQueryValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
