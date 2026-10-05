namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;

public class GetDependencyByPOIdValidator : AbstractValidator<GetDependencyByPOIdRequest>
{
    public GetDependencyByPOIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}