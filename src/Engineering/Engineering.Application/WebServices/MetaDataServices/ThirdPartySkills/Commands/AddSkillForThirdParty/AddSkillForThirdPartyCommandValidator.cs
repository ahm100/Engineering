
namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Commands.AddSkillForThirdPartyCommand;

public class AddSkillForThirdPartyCommandValidator : AbstractValidator<AddSkillForThirdPartyCommand>
{
    public AddSkillForThirdPartyCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.SkillId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}
