using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetThirdPartyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;

public class UpdateThirdPartyCommand() : ICommand<UpdateThirdPartyResponse?>
{
    public UpdateTransportationContractorRequest Item { get; set; }
    public ThirdPartyUserModel? ThirdParty { get; set; }
    public UpdateThirdPartyCommand(
        UpdateTransportationContractorRequest item,
        ThirdPartyUserModel? thirdParty) : this()
    {
        Item = item;
        ThirdParty = thirdParty;
    }
}