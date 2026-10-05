namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;

public class GetProjectOperationProgressValidator : AbstractValidator<GetProjectOperationProgressRequest>
{
    public GetProjectOperationProgressValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
