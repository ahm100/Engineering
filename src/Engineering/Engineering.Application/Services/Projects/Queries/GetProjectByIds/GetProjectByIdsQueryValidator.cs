namespace Engineering.Application.Services.Projects.Queries.GetProjectByIds;

public class GetProjectByIdsQueryValidator : AbstractValidator<GetProjectByIdsQuery>
{
    public GetProjectByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}
