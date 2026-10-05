
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillByCode;

public class GetSkillByCodeQueryValidator : AbstractValidator<GetSkillByCodeQuery>
{
    public GetSkillByCodeQueryValidator()
    {
        RuleFor(oo => oo.Code).NotEmpty().WithError(MetaDataErrors.CodeIsEmpty);
    }
}
