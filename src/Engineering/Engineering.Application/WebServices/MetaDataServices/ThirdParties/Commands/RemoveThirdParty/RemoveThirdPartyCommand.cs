using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.RemoveThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.RemoveThirdParty;


public record RemoveThirdPartyCommand(long Id) : ICommand<RemoveThirdPartyResponse?>;