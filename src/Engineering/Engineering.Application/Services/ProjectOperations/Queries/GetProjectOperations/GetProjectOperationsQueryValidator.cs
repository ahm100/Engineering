
namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperations;

public class GetProjectOperationsQueryValidator : AbstractValidator<GetProjectOperationsQuery>
{
    public GetProjectOperationsQueryValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}
