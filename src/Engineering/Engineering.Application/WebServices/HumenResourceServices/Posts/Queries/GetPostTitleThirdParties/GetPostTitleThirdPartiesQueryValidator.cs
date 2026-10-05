namespace Engineering.Application.WebServices.HumenResourceServices.Posts.Queries.GetPostTitleThirdParties;

public class GetPostTitleThirdPartiesQueryValidator : AbstractValidator<GetPostTitleThirdPartiesQuery>
{
    public GetPostTitleThirdPartiesQueryValidator()
    {
        RuleForEach(x => x.ThirdPartyIds)
            .IsPositive(GlobalCmts.Id);
    }
}
