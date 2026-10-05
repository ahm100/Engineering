
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkills;

public class GetFilteredSkillsQueryValidator : AbstractValidator<GetFilteredSkillsQuery>
{
    public GetFilteredSkillsQueryValidator()
    {
        //RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        //RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(Validating.Zero).WithError(Validating.PageIndexNotValid).LessThanOrEqualTo(Validating.MaxIndex).WithError(Validating.PageIndexNotValid);
        //RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(Validating.Zero).WithError(Validating.PageSizeNotValid).LessThanOrEqualTo(Validating.MaxSize).WithError(Validating.PageSizeNotValid);
    }
}