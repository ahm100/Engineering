
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;

public class GetSkillByIdQueryValidator : AbstractValidator<GetSkillByIdQuery>
{
    public GetSkillByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}