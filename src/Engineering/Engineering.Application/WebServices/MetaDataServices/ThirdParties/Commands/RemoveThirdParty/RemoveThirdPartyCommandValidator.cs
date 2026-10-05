namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.RemoveThirdParty;


public class RemoveThirdPartyCommandValidator : AbstractValidator<RemoveThirdPartyCommand>
{
    public RemoveThirdPartyCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MetaDataErrors.IdIsEmpty);
    }
}
