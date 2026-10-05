
namespace Engineering.Application.Services.ProjectOperations.Queries.GetPOsWithoutInclude;

public class GetPOsWithoutIncludeQueryValidator : AbstractValidator<GetPOsWithoutIncludeQuery>
{
    public GetPOsWithoutIncludeQueryValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}
