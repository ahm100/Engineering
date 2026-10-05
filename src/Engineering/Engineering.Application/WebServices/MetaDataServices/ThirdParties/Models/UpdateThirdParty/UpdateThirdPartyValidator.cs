namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

public class UpdateThirdPartyValidator : AbstractValidator<UpdateThirdPartyRequest>
{
    public UpdateThirdPartyValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MetaDataErrors.IdIsEmpty);
    }
}