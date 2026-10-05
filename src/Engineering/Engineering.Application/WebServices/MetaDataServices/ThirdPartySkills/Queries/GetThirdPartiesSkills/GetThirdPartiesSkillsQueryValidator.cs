namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Queries.GetThirdPartiesSkills;

public class GetThirdPartiesSkillsQueryValidator : AbstractValidator<GetThirdPartiesSkillsQuery>
{
    public GetThirdPartiesSkillsQueryValidator()
    {
        RuleForEach(oo => oo.ThirdPartyIds).IsPositive(GlobalCmts.ThirdParty);
    }
}